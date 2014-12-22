using System.Web.Http;

namespace PollApi
{
    public class Bootstrap
    {
        public void Configure(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
              name: "Poll",
              routeTemplate: "{pollId}",
              defaults: new
              {
                  controller = "Poll",
              });

            config.Routes.MapHttpRoute(
              name: "Default",
              routeTemplate: "{controller}/{id}",
              defaults: new
              {
                  controller = "Poll",
                  id = RouteParameter.Optional
              });
        }
    }
}