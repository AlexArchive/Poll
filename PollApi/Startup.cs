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

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Filters.Add(new ValidModelActionFilter());
            //config.Services.Replace(typeof(IHttpControllerActivator), new CompositionRoot());

            var container = new WindsorContainer().Install(new WindsorInstaller());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{pollId}",
                defaults: new { pollId = RouteParameter.Optional }
            );

            FluentValidationModelValidatorProvider
                .Configure(config, provider => provider.ValidatorFactory = new WindsorValidatorFactory(container));

            config.DependencyResolver = new WindsorResolver(container);
            app.UseWebApi(config);
        }
    }
}