﻿using System.Threading.Tasks;
using zxm.WeChat.Extensions.Models;

namespace zxm.WeChat.Extensions
{
    /// <summary>
    /// ApiHelper interface
    /// </summary>
    public interface IWeChatApiHelper
    {
        /// <summary>
        /// AppId of WeChat
        /// </summary>
        string AppId { get; }

        /// <summary>
        /// Secret of WeChat
        /// </summary>
        string AppSecret { get; }

        /// <summary>
        /// Get access token
        /// </summary>
        /// <returns></returns>
        Task<string> GetAccessToken();

        /// <summary>
        /// Get js api ticket
        /// </summary>
        /// <returns></returns>
        Task<string> GetJsApiTicket();

        /// <summary>
        /// Get js sdk signature
        /// </summary>
        /// <param name="currentPageUrl"></param>
        /// <returns></returns>
        Task<JsSdkSignature> GetJsSdkSignature(string currentPageUrl);
    }
}
