using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using PollApi;
using System.Web.Http;
using System.Web.Http.Dispatcher;

[assembly: OwinStartup(typeof(Startup))]

namespace PollApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Services.Replace(typeof(IHttpControllerActivator), new CompositionRoot());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{pollId}",
                defaults: new { pollId = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}