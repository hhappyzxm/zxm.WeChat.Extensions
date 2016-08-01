using System;

namespace zxm.WeChat.Extensions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
        }
    }
}
