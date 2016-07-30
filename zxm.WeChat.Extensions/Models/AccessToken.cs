using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace zxm.WeChat.Extensions.Models
{
    /// <summary>
    /// AccessToken
    /// Example:{"access_token":"RWR-bozRjozxfnb2muGzPDrp-ZhMQCXWnlDXqCda5VQRCgh1fCRKlXRAmXcOJk_1E8ebfZF-qTv_78AaFzS55BAeXG9eJsggNGtW3En96a6wW-IoAGF5m3Ufwx4-bqYOBGNhAAAEJQ","expires_in":7200}
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// Constructor of AccessToken
        /// </summary>
        /// <param name="token"></param>
        /// <param name="expiresIn"></param>
        [JsonConstructor]
        public AccessToken(string token, int expiresIn):this(token, expiresIn, DateTime.Now)
        {
            
        }

        /// <summary>
        /// Constructor of AccessToken
        /// </summary>
        /// <param name="token"></param>
        /// <param name="expiresIn"></param>
        /// <param name="gotTime"></param>
        public AccessToken(string token, int expiresIn, DateTime gotTime)
        {
            Token = token;
            ExpiresIn = expiresIn;
            GotTime = gotTime;
        }

        /// <summary>
        /// Token
        /// </summary>
        [JsonProperty("access_token")]
        public string Token { get; set; }

        /// <summary>
        /// Expires in seconds
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Got access token time
        /// </summary>
        public DateTime GotTime { get; set; }

        /// <summary>
        /// Because of the token from WeChat has expiry time, so we need to check whether the token is expired.
        /// </summary>
        /// <param name="aheadRefreshSeconds"></param>
        /// <returns></returns>
        public  bool NeedRefreshToken(int aheadRefreshSeconds = 10)
        {
            return ExpiresIn - (DateTime.Now - GotTime).Seconds >= aheadRefreshSeconds;
        }
    }
}
