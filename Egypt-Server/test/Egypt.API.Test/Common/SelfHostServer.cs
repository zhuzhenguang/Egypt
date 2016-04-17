using System;
using System.Web.Http.SelfHost;
using Autofac;

namespace Egypt.API.Test.Common
{
    public class SelfHostServer
    {
        private readonly HttpSelfHostServer _hostServer;
        public Uri BaseAddress { get; private set; }
        public IContainer RootContainer { get; private set; }

        public SelfHostServer(int port)
        {
            BaseAddress = new UriBuilder("http", "localhost", port).Uri;
            var config = new HttpSelfHostConfiguration(BaseAddress);

            RootContainer = EgyptApi.BootStrap(config);
            _hostServer = new HttpSelfHostServer(config);
            _hostServer.OpenAsync().Wait();
        }

        public void Close()
        {
            _hostServer.CloseAsync().Wait();
        }
    }
}