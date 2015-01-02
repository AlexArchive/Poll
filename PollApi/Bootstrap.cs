using System.Web.Http;
using System.Web.Http.Dispatcher;
using Newtonsoft.Json.Serialization;

namespace PollApi
{
    public class Bootstrap
    {
        public void Configure(HttpConfiguration config)
        {
            SetControllerActivator(config);
            
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
              name: "Default",
              routeTemplate: "{controller}/{pollId}",
              defaults: new
              {
                  controller = "Poll",
                  pollId = RouteParameter.Optional
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