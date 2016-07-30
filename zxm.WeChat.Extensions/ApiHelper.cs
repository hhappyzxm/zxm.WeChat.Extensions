using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using zxm.WeChat.Extensions.Models;
using zxm.AsyncLock;

namespace zxm.WeChat.Extensions
{
    /// <summary>
    /// Api of WeChat
    /// </summary>
    public class ApiHelper
    {
        private readonly Locker _lock = new Locker();
   
        private AccessToken _accessToken;

        /// <summary>
        /// Constructor of ApiHelper
        /// </summary>
        public ApiHelper(string appId, string appSecret)
        {
            if (string.IsNullOrEmpty(appId))
            {
                throw new ArgumentNullException(nameof(appId));
            }

            if (string.IsNullOrEmpty(appSecret))
            {
                throw new ArgumentNullException(nameof(appSecret));
            }

            AppId = appId;
            AppSecret = appSecret;
        }

        /// <summary>
        /// AppId of WeChat
        /// </summary>
        public string AppId { get; }

        /// <summary>
        /// Secret of WeChat
        /// </summary>
        public string AppSecret { get; }

        /// <summary>
        /// Get access token
        /// </summary>
        /// <returns></returns>
        public async Task<AccessToken>  GetAccessToken()
        {
            using (var releaser = await _lock.LockAsync())
            {
                if (_accessToken == null || _accessToken.NeedRefreshToken())
                {
                    using (var httpClient = new HttpClient())
                    {
                        var url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={AppId}&secret={AppSecret}";
                        var jsonString = await httpClient.GetStringAsync(url);

                        if (!TryCatchError(jsonString))
                        {
                            _accessToken = JsonConvert.DeserializeObject<AccessToken>(jsonString);
                        }
                    }
                }

                return _accessToken;
            }
        }

        private bool TryCatchError(string jsonString)
        {
            try
            {
                var err = JsonConvert.DeserializeObject<ErrorResult>(jsonString);
                throw new ApiException($"{err.Code}-{err.Message}");
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }
    }
}
