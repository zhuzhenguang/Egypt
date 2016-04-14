using System.Net.Http;
using System.Web.Http;

namespace Egypt.API
{
    public static class RouteConfig
    {
        public static void Map(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "user-register",
                "user",
                new
                {
                    controller = "User",
                    action = "Register",
                    method = HttpMethod.Post
                });
        }
    }
}