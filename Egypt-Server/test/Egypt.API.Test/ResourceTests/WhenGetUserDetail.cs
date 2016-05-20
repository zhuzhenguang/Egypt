using System.Net;
using Egypt.API.Resources;
using Egypt.API.Test.Common;
using Egypt.Domain;
using Xunit;

namespace Egypt.API.Test.ResourceTests
{
    public class WhenGetUserDetail : TestBase
    {
        [Fact]
        public void should_return_user_detail_when_request_by_valid_id()
        {
            var requestForRegister = new UserRegisterRequest("zhu", "zhu@qq.com", "zhuzhu", Gender.Male);
            var responseForRegister = Post("user", requestForRegister);
            Assert.Equal(HttpStatusCode.OK, responseForRegister.StatusCode);
            var links = Body<UserRegisterResult>(responseForRegister).Links;

            var response = Get(links[0]);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var userDto = Body<UserDto>(response);
            Assert.Equal("zhu", userDto.Name);
            Assert.Equal("zhu@qq.com", userDto.Email);
            Assert.Equal(Gender.Male, userDto.Gender);
        }

        [Fact]
        public void should_return_not_found_when_request_with_invalid_id()
        {
            var inValidLink = new ResourceLink("user/detail", "users/-1");
            var response = Get(inValidLink);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}