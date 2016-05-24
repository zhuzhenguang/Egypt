using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Autofac;
using Egypt.API.Resources;
using Egypt.API.Test.ResourceTests;
using Newtonsoft.Json;

namespace Egypt.API.Test.Common
{
    public class TestBase : IDisposable
    {
        private readonly SelfHostServer server;
        protected ILifetimeScope Scope { get; private set; }

        protected TestBase()
        {
            server = new SelfHostServer(21000);
            Scope = server.RootContainer.BeginLifetimeScope();
        }

        protected static T Body<T>(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            var userResult = JsonConvert.DeserializeObject<T>(result);
            return userResult;
        }

        protected HttpResponseMessage Post(string uri, object request)
        {
            return Send(
                uri,
                HttpMethod.Post,
                httpRequest =>
                {
                    httpRequest.Content = new StringContent(JsonConvert.SerializeObject(request));
                    httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                });
        }

        protected HttpResponseMessage Get(ResourceLink uri)
        {
            return Get(uri.Uri);
        }
        
        private HttpResponseMessage Send(string uri, HttpMethod method, Action<HttpRequestMessage> action = null)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(server.BaseAddress + uri)
            };
            if (action != null)
            {
                action(httpRequest);
            }
            return new HttpClient().SendAsync(httpRequest).Result;
        }

        private HttpResponseMessage Get(string uri)
        {
            return Send(uri, HttpMethod.Get);
        }

        protected static string ErrorMessageFrom(HttpResponseMessage userRegisterResponse)
        {
            return Body<ErrorMessage>(userRegisterResponse).Message;
        }

        public void Dispose()
        {
            server.Close();
        }
    }
}