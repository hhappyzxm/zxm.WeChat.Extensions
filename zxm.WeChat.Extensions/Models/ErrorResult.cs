using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public ErrorResult(string code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Error Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; set; }
    }
}
