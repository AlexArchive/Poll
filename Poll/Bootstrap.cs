using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Poll
{
    public class Bootstrap
    {
        public void Configure(HttpConfiguration config)
        {
            SetControllerActivator(config);
            
            config.Formatters.Remove(config.Formatters.XmlFormatter);

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

        protected virtual void SetControllerActivator(HttpConfiguration config)
        {
            config.Services.Replace(
                typeof(IHttpControllerActivator), 
                new CompositionRoot());
        }
    }
}