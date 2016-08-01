using System;

namespace zxm.WeChat.Extensions
{
    public class WeChatApiException : Exception
    {
        public WeChatApiException(string message) : base(message)
        {
        }
    }
}
