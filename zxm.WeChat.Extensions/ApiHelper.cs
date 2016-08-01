using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using zxm.WeChat.Extensions.Models;
using zxm.AsyncLock;

namespace zxm.WeChat.Extensions
{
    /// <summary>
    /// Api of WeChat
    /// </summary>
    public class ApiHelper : IApiHelper
    {
        private readonly Locker _accessTokenLock = new Locker();
        private readonly Locker _jsApiTicketLock = new Locker();
        private AccessToken _accessToken;
        private JsApiTicket _jsApiTicket;


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
        public async Task<string>  GetAccessToken()
        {
            using (var releaser = await _accessTokenLock.LockAsync())
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

                return _accessToken.Token;
            }
        }

        /// <summary>
        /// Get js api ticket
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetJsApiTicket()
        {
            using (var releaser = await _jsApiTicketLock.LockAsync())
            {
                if (_jsApiTicket == null || _jsApiTicket.NeedRefreshToken())
                {
                    using (var httpClient = new HttpClient())
                    {
                        var token = await GetAccessToken();
                        var url = $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={token}&type=jsapi";
                        var jsonString = await httpClient.GetStringAsync(url);

                        if (!TryCatchError(jsonString))
                        {
                            _jsApiTicket = JsonConvert.DeserializeObject<JsApiTicket>(jsonString);
                        }
                    }
                }

                return _jsApiTicket.Ticket;
            }
        }

        /// <summary>
        /// Get js sdk signature
        /// </summary>
        /// <param name="currentPageUrl"></param>
        /// <returns></returns>
        public async Task<JsSdkSignature> GetJsSdkSignature(string currentPageUrl)
        {
            var ticket = await GetJsApiTicket();
            return new JsSdkSignature(ticket, currentPageUrl);
        }

        /// <summary>
        /// Try to catch error from api of WeChat
        /// Example: {"errcode":40013,"errmsg":"invalid appid"}
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        private bool TryCatchError(string jsonString)
        {
            var err = JObject.Parse(jsonString);
            JToken codeJToken;
            if (err.TryGetValue("errcode", out codeJToken))
            {
                if (codeJToken.ToObject<string>() == "0")
                {
                    return false;
                }

                JToken messageJToken;
                if (err.TryGetValue("errmsg", out messageJToken))
                {
                    throw new ApiException($"{codeJToken}-{messageJToken}");
                }
                else
                {
                    throw new ApiException($"Parse invalid - {jsonString}");
                }
            }

            return false;
        }
    }
}
