using Castle.Windsor;
using FluentValidation.WebApi;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using PollApi;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace PollApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureRoute(config);
            ConfigureFormatters(config);
            ConfigureGlobalFilters(config);
            ConfigureCompositionRoot(config);

            app.UseWebApi(config);
        }

        private static void ConfigureRoute(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{pollId}",
                defaults: new { pollId = RouteParameter.Optional }
            );
        }

        private static void ConfigureFormatters(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = 
                new CamelCasePropertyNamesContractResolver();
        }

        private static void ConfigureGlobalFilters(HttpConfiguration config)
        {
            config.Filters.Add(new ValidModelActionFilter());
        }

        private static void ConfigureCompositionRoot(HttpConfiguration config)
        {
            var container = new WindsorContainer().Install(new WindsorInstaller());

            FluentValidationModelValidatorProvider.Configure(
                config, provider => provider.ValidatorFactory = new WindsorValidatorFactory(container));

            config.DependencyResolver = new WindsorResolver(container);
        }
    }
}