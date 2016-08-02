using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using zxm.WeChat.Extensions;

namespace zxm.WeChat.Extensions.Tests
{
    public class TestApiHelper
    {
        private const string _appId = "wxe97686e0b39f0fc9";
        private const string _secret = "e611bac1062498a010df380ddb6d92b5";

        private WeChatApiHelper _apiHelper;

        public TestApiHelper()
        {
            WeChatApiHelper.Register(_appId, _secret);
            _apiHelper = WeChatApiHelper.Instance;
        }

        [Fact]
        public async Task TestGetAccessToken()
        {
            await _apiHelper.GetAccessToken();

            //apiHelper = new WeChatApiHelper(_appId, "1111");
            //await Assert.ThrowsAsync<WeChatApiException>(async () => await apiHelper.GetAccessToken());
        }

        [Fact]
        public async Task TestGetJsApiTicket()
        {
            await _apiHelper.GetJsApiTicket();
        }

        [Fact]
        public async Task TestGetJsSdkSignature()
        {
            await _apiHelper.GetJsSdkSignature("http://www.webezi.com.au");
        }

        [Fact]
        public async Task TaskApiException()
        {
            var type = _apiHelper.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            

            await Assert.ThrowsAsync<WeChatApiException>(async () => await WeChatApiHelper.Instance.GetAccessToken());
        }
    }
}
