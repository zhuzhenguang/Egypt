using System.Net;
using Autofac;
using Egypt.API.Resources;
using Egypt.API.Test.Common;
using Egypt.Domain;
using NHibernate;
using Xunit;

namespace Egypt.API.Test.ResourceTests
{
    public class WhenRegisterUser : TestBase
    {
        [Fact]
        public void should_return_link_when_register_with_correct_input()
        {
            var request = new UserRegisterRequest("zhu", "zhu@qq.com", "zhuzhu", Gender.Male);
            var response = Post("user", request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var links = Body<UserRegisterResult>(response).Links;
            Assert.Equal(1, links.Count);
            Assert.Equal("user/detail", links[0].Rel);
            Assert.Equal("users/1", links[0].Uri);

            //resolve component of register in container;
            var session = Scope.Resolve<ISession>();

            var userId = links[0].ExtractId();
            var user = session.Get<User>(userId);  
            Assert.Equal("zhu", user.Name);
            Assert.Equal("zhu@qq.com", user.Email);
            Assert.Equal("zhuzhu", user.Password);
            Assert.Equal(Gender.Male, user.Gender);
        }

        [Fact]
        public void should_return_bad_request_when_register_with_any_empty_input()
        {
            var emptyNameResponse = Post("user", new UserRegisterRequest("", "zhu@qq.com", "zhuzhu", Gender.Male));
            Assert.Equal(HttpStatusCode.BadRequest, emptyNameResponse.StatusCode);

            var emptyEmailResponse = Post("user", new UserRegisterRequest("zhu", "", "zhuzhu", Gender.Male));
            Assert.Equal(HttpStatusCode.BadRequest, emptyEmailResponse.StatusCode);

            var emptyPasswordResponse = Post("user", new UserRegisterRequest("zhu", "zhu@qq.com", "", Gender.Male));
            Assert.Equal(HttpStatusCode.BadRequest, emptyPasswordResponse.StatusCode);
        }

        [Fact]
        public void should_return_bad_request_when_register_with_an_existing_email()
        {
            var firstUserRegisterResponse = Post("user", new UserRegisterRequest("zhu", "zhu@qq.com", "zhuzhu", Gender.Male));
            Assert.Equal(HttpStatusCode.OK, firstUserRegisterResponse.StatusCode);

            var secondUserRegisterResponse = Post("user", new UserRegisterRequest("zhu1", "zhu@qq.com", "zhuzhu1", Gender.Female));
            Assert.Equal(HttpStatusCode.BadRequest, secondUserRegisterResponse.StatusCode);
        }
    }
}