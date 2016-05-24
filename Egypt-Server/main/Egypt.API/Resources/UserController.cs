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
        private readonly ISession session;

        public UserController(ISession session)
        {
            this.session = session;
        }

        [HttpPost]
        public HttpResponseMessage Register(UserRegisterRequest request)
        {
            ValidateRequest(request);

            var user = new User(request.Name, request.Email, request.Password, request.Gender);
            using (var tx = session.BeginTransaction())
            {
                session.Save(user);
                tx.Commit();
            }

            var userResult = new UserRegisterResult();
            userResult.AddLink("user/detail", string.Format("users/{0}", user.Id));
            return Request.CreateResponse(HttpStatusCode.OK, userResult);
        }

        [HttpGet]
        public HttpResponseMessage Show(long id)
        {
            var user = session.Get<User>(id);
            if (user == null)
            {
                throw new NotFoundException("Please input a valid user id");
            }

            var userDto = new UserDto(user);
            return Request.CreateResponse(HttpStatusCode.OK, (object) userDto);
        }

        [HttpPost]
        public HttpResponseMessage Login(UserLoginRequest request)
        {
            var user = session.Query<User>()
                           .FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

            return Request.CreateResponse(user == null ? HttpStatusCode.BadRequest : HttpStatusCode.OK);
        }

        private void ValidateRequest(UserRegisterRequest request)
        {
            if (request.AnyBlank())
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
            return session.Query<User>().Any(u => u.Email.Equals(email));
        }
    }
}