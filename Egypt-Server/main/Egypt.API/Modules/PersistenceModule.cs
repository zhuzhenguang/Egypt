using System.IO;
using Autofac;
using Egypt.Domain;
using NHibernate;
using NHibernate.Cfg;

namespace Egypt.API.Modules
{
    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(BuildSessionFactory).SingleInstance().As<ISessionFactory>();
            builder.Register(BuildSession).InstancePerLifetimeScope().As<ISession>();
        }

        private static ISessionFactory BuildSessionFactory(IComponentContext componentContext)
        {
            var configuration = new Configuration()
                    .SetDefaultAssembly(typeof (User).Assembly.FullName)
                    .SetDefaultNamespace(typeof (User).Namespace)
                    .AddDirectory(new DirectoryInfo("./"));

            return configuration.BuildSessionFactory();
        }

        private static ISession BuildSession(IComponentContext componentContext)
        {
            var sessionFactory = componentContext.Resolve<ISessionFactory>();
            return sessionFactory.OpenSession();
        }
    }
}