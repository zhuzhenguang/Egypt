using System.Net;
using System.Net.Http;
using System.Web.Http;
using Egypt.Domain;
using NHibernate;

namespace Egypt.API.Resources
{
    public class UserController : ApiController
    {
        private readonly ISession session;

        public UserController(ISession session)
        {
            this.session = session;
        }

        public HttpResponseMessage Register(UserRegisterRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Gender = request.Gender
            };

            using (var tx = session.BeginTransaction())
            {
                session.Save(user);
                tx.Commit();
            }

            var userResult = new UserRegisterResult();
            userResult.AddLink("user/detail", string.Format("users/{0}", user.Id));
            return Request.CreateResponse(HttpStatusCode.OK, userResult);
        }
    }
}