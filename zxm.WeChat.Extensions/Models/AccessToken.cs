using System;

namespace zxm.WeChat.Extensions.Models
{
    /// <summary>
    /// AccessToken
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// Constructor of AccessToken
        /// </summary>
        /// <param name="token"></param>
        /// <param name="expiresIn"></param>
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
        public string Token { get; set; }

        /// <summary>
        /// Expires in seconds
        /// </summary>
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
