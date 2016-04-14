using System;
using System.Web.Http.SelfHost;
using Autofac;

namespace Egypt.API.Test.ResourceTests
{
    public class SelfHostServer
    {
        private readonly HttpSelfHostServer hostServer;
        public Uri BaseAddress { get; private set; }
        public IContainer RootContainer { get; private set; }

        public SelfHostServer(int port)
        {
            BaseAddress = new UriBuilder("http", "localhost", port).Uri;
            var config = new HttpSelfHostConfiguration(BaseAddress);

            RootContainer = EgyptApi.BootStrap(config);
            hostServer = new HttpSelfHostServer(config);
            hostServer.OpenAsync().Wait();
        }

        public void Close()
        {
            hostServer.CloseAsync().Wait();
        }
    }
}