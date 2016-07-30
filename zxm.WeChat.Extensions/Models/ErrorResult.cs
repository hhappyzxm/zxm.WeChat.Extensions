using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace zxm.WeChat.Extensions.Models
{
    /// <summary>
    /// Error result from WeChat Api
    /// Example: {"errcode":40013,"errmsg":"invalid appid"}
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// Constructor of ErrorResult
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        [JsonConstructor]
        public ErrorResult(string code, string message)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            Code = code;
            Message = message;
        }

        /// <summary>
        /// Error Code
        /// </summary>
        [JsonProperty("errcode")]
        public string Code { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        [JsonProperty("errmsg")]
        public string Message { get; set; }
    }
}
