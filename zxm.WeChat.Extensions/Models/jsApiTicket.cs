using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace zxm.WeChat.Extensions.Models
{
    /// <summary>
    /// JsApiTicket
    /// Example:
    /// {
    /// "errcode":0,
    /// "errmsg":"ok",
    /// "ticket":"bxLdikRXVbTPdHSM05e5u5sUoXNKd8-41ZO3MhKoyN5OfkWITDGgnr2fwJ0m9E8NYzWKVZvdVtaUgWvsdshFKA",
    /// "expires_in":7200
    /// }
    /// </summary>
    public class JsApiTicket
    {
        /// <summary>
        /// Constructor of JsApiTicket
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="expiresIn"></param>
        [JsonConstructor]
        public JsApiTicket(string ticket, int expiresIn)
        {
            if (string.IsNullOrEmpty(ticket))
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            Ticket = ticket;
            ExpiresIn = expiresIn;
            GotTime = DateTime.Now;
        }

        /// <summary>
        /// Token
        /// </summary>
        [JsonProperty("ticket")]
        public string Ticket { get; set; }

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
        /// Because of the ticket from WeChat has expiry time, so we need to check whether the ticket is expired.
        /// </summary>
        /// <param name="aheadRefreshSeconds"></param>
        /// <returns></returns>
        public bool NeedRefreshToken(int aheadRefreshSeconds = 10)
        {
            return ExpiresIn - (DateTime.Now - GotTime).Seconds >= aheadRefreshSeconds;
        }
    }
}
