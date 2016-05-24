using System.Net;
using Egypt.API.Resources;
using Egypt.API.Test.Common;
using Egypt.Domain;
using Xunit;

namespace Egypt.API.Test.ResourceTests
{
    public class WhenLoginUser : TestBase
    {
        [Fact]
        public void should_return_ok_when_login_with_valid_username_or_password()
        {
            var registerRequest = new UserRegisterRequest("zhu", "zhu@qq.com", "zhuzhu", Gender.Male);
            var registerResponse = Post("user", registerRequest);
            Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

            var loginRequest = new UserLoginRequest("zhu@qq.com", "zhuzhu");
            var loginResponse = Post("login", loginRequest);
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("jiao@qq.com", null)]
        [InlineData(-1, null)]
        [InlineData("zhu@qq.com", "jiao")]
        public void should_return_bad_request_when_login_with_wrong_username_and_right_password(string email, string password)
        {
            var registerRequest = new UserRegisterRequest("zhu", "zhu@qq.com", "zhuzhu", Gender.Male);
            var registerResponse = Post("user", registerRequest);
            Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

            var loginRequest = new UserLoginRequest(email, password);
            var loginResponse = Post("login", loginRequest);
            Assert.Equal(HttpStatusCode.BadRequest, loginResponse.StatusCode);
        }
    }
}