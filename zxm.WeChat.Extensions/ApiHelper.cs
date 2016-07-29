using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using zxm.WeChat.Extensions.Models;

namespace zxm.WeChat.Extensions
{
    /// <summary>
    /// Api of WeChat
    /// </summary>
    public class ApiHelper
    {
        private readonly object _lock = new object();
        private AccessToken _accessToken;

        /// <summary>
        /// Constructor of ApiHelper
        /// </summary>
        private ApiHelper(string appId, string appSecret)
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

        private AccessToken GetAccessToken()
        {
            lock (_lock)
            {
                if (_accessToken == null || _accessToken.NeedRefreshToken())
                {
                    using (var httpClient = new HttpClient())
                    {
                        var url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={AppId}&secret={AppSecret}";
                        var resString = httpClient.GetStringAsync(url);
                     //   var responseObj = new JsonConverter
                     var a = new Dictionary<string, string>().ToList();
                    }
                }

                return _accessToken;
            }
        }

        private bool TryCatchError(IDictionary<string, string> data, out ErrorResult result)
        {
            string errCode;
            if(data.TryGetValue("errcode", out errCode))
            {
                string errMessage;
                if (data.TryGetValue("errmsg", out errMessage))
                {
                    result = new ErrorResult(errCode, errMessage);
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}
