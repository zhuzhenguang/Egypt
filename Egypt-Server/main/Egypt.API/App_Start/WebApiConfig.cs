using System.Web.Http;
using Egypt.API.Exception;

namespace Egypt.API
{
    public static class WebApiConfig
    {
        public static void RegisterFilters(HttpConfiguration config)
        {
            var filters = config.Filters;
            filters.Add(new BadRequestResponseFilter());
        }
    }
}