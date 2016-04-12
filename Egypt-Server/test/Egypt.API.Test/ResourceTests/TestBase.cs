using System.IO;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Egypt.API.Resources;
using Egypt.Domain;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Cfg;

namespace Egypt.API.Test.ResourceTests
{
    public class TestBase
    {
        private readonly ISessionFactory _sessionFactory;

        protected TestBase()
        {
            var configuration =
                new Configuration()
                    .SetDefaultAssembly(typeof(User).Assembly.FullName)
                    .SetDefaultNamespace(typeof(User).Namespace)
                    .AddDirectory(new DirectoryInfo("./"));

            _sessionFactory = configuration.BuildSessionFactory();
        }

        protected ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

        protected UserController ResolveController()
        {
            var httpConfiguration = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/user");
            var controller = new UserController(GetSession())
            {
                Request = request
            };
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
            return controller;
        }

        protected static T Body<T>(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            var userResult = JsonConvert.DeserializeObject<T>(result);
            return userResult;
        }
    }
}