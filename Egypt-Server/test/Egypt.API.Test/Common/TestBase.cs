using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Autofac;
using Egypt.API.Resources;
using Newtonsoft.Json;

namespace Egypt.API.Test.Common
{
    public class TestBase: IDisposable
    {
        private readonly SelfHostServer _server;
        protected ILifetimeScope Scope { get; private set; }

        protected TestBase()
        {
            _server = new SelfHostServer(21000);
            Scope = _server.RootContainer.BeginLifetimeScope();
        }

        protected static T Body<T>(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            var userResult = JsonConvert.DeserializeObject<T>(result);
            return userResult;
        }

        protected HttpResponseMessage Post(string uri, UserRegisterRequest request)
        {
            var httpRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(request)),
                Method = HttpMethod.Post,
                RequestUri = new Uri( _server.BaseAddress + uri)
            };
            httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return new HttpClient().SendAsync(httpRequest).Result;
        }

        public void Dispose()
        {
            _server.Close();
        }
    }
}