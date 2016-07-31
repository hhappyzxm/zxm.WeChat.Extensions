using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using zxm.WeChat.Extensions;

namespace zxm.WeChat.Extensions.Tests
{
    public class TestApiHelper
    {
        private const string _appId = "wxe97686e0b39f0fc9";
        private const string _secret = "e611bac1062498a010df380ddb6d92b5";

        [Fact]
        public async Task TestGetAccessToken()
        {
            var apiHelper = new ApiHelper(_appId, _secret);
            await apiHelper.GetAccessToken();

            apiHelper = new ApiHelper(_appId, "1111");
            await Assert.ThrowsAsync<ApiException>(async () => await apiHelper.GetAccessToken());
        }

        [Fact]
        public async Task TestGetJsApiTicket()
        {
            var apiHelper = new ApiHelper(_appId, _secret);
            await apiHelper.GetJsApiTicket();
        }
    }
}
