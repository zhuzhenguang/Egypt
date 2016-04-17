using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Egypt.API.Exception;
using Egypt.Domain;
using NHibernate;
using NHibernate.Linq;

namespace Egypt.API.Resources
{
    public class UserController : ApiController
    {
        private readonly ISession _session;

        public UserController(ISession session)
        {
            _session = session;
        }

        [HttpPost]
        public HttpResponseMessage Register(UserRegisterRequest request)
        {
            ValidateRequest(request);

            var user = new User(request.Name, request.Email, request.Password, request.Gender);
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(user);
                tx.Commit();
            }

            var userResult = new UserRegisterResult();
            userResult.AddLink("user/detail", string.Format("users/{0}", user.Id));
            return Request.CreateResponse(HttpStatusCode.OK, userResult);
        }

        private void ValidateRequest(UserRegisterRequest request)
        {
            if (request.Name == string.Empty || request.Email == string.Empty || request.Password == string.Empty)
            {
                throw new BadRequestException("Should register with correct information");
            }

            if (IsExistedEmail(request.Email))
            {
                throw new BadRequestException("This email has been used, please change!");
            }
        }

        private bool IsExistedEmail(string email)
        {
            var existingEmails = _session.Query<User>().Select(u => u.Email).ToList();
            return existingEmails.Any(e => e.Equals(email));
        }
    }
}