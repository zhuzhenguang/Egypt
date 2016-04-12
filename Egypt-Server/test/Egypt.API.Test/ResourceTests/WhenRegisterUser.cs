using System.Net;
using Egypt.API.Resources;
using Egypt.Domain;
using Xunit;

namespace Egypt.API.Test.ResourceTests
{
    public class WhenRegisterUser : TestBase
    {
        [Fact]
        public void should_register_user()
        {
            var controller = ResolveController();

            var response = controller.Register(
                new UserRegisterRequest
                {
                    Name = "zhu",
                    Email = "zhu@qq.com",
                    Password = "zhuzhu",
                    Gender = Gender.Male
                });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var userResult = Body<UserRegisterResult>(response);
            Assert.Equal(1, userResult.Links.Count);
            Assert.Equal("user/detail", userResult.Links[0].Rel);
            Assert.Equal("users/1", userResult.Links[0].Uri);

            var userId = userResult.Links[0].ExtractId();
            var user = GetSession().Get<User>(userId);
            Assert.Equal("zhu", user.Name);
            Assert.Equal("zhu@qq.com", user.Email);
            Assert.Equal("zhuzhu", user.Password);
            Assert.Equal(Gender.Male, user.Gender);
        }
    }
}