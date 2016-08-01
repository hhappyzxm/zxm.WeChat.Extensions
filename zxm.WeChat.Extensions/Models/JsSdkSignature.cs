using System;
using System.Security.Cryptography;
using System.Text;

namespace zxm.WeChat.Extensions.Models
{
    /// <summary>
    /// Js sdk signature
    /// </summary>
    public class JsSdkSignature
    {
        /// <summary>
        /// Constructor of JsSdkSignature
        /// </summary>
        /// <param name="jsApiTicket"></param>
        /// <param name="currentPageUrl"></param>
        public JsSdkSignature(string jsApiTicket, string currentPageUrl)
        {
            if (string.IsNullOrEmpty(jsApiTicket))
            {
                throw new ArgumentNullException(nameof(jsApiTicket));
            }

            NonceStr = Guid.NewGuid().ToString().Replace("-", "");
            JsApiTicket = jsApiTicket;
            CurrentPageUrl = currentPageUrl;
            TimeStamp = GetCurrentTimeSpan();
            Signature = Sha1($"jsapi_ticket={JsApiTicket}&noncestr={NonceStr}&timestamp={TimeStamp}&url={CurrentPageUrl}");
        }

        /// <summary>
        /// Random string
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// Js Api Ticket
        /// </summary>
        public string JsApiTicket { get; }

        /// <summary>
        /// Time stamp of current date time
        /// </summary>
        public int TimeStamp { get; }

        /// <summary>
        /// Current page url
        /// </summary>
        public string CurrentPageUrl { get; }

        /// <summary>
        /// Signature
        /// </summary>
        public string Signature { get; }

        /// <summary>
        /// Get current time stamp
        /// </summary>
        /// <returns></returns>
        private int GetCurrentTimeSpan()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds);
        }

        /// <summary>
        /// Sha1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string Sha1(string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            var data = SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
