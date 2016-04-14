using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Egypt.API.Modules;

namespace Egypt.API
{
    public class EgyptApi : HttpApplication
    {
        public static IContainer BootStrap(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterModule<PersistenceModule>();
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            RouteConfig.Map(config);
            return container;
        }
    }
}