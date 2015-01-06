using System.Web.Http;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using Raven.Client;
using Raven.Client.Document;

namespace PollApi
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<ApiController>()
                    .WithServiceSelf()
                    .LifestylePerWebRequest());
            container.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(AbstractValidator<>))
                .WithServiceFirstInterface()
                .LifestylePerWebRequest());
            container.Register(
                Component.For<IDocumentStore>().Instance(CreateDocumentStore()).LifeStyle.Singleton,
                Component.For<IDocumentSession>().UsingFactoryMethod(GetDocumentSesssion).LifeStyle.PerWebRequest
            );




            //container.Register(
            //    Classes.FromThisAssembly()
            //        .BasedOn(typeof(AbstractValidator<>))
            //        .WithServiceFirstInterface()
            //        .LifestylePerWebRequest());
        }

        private static IDocumentStore CreateDocumentStore()
        {
            return new DocumentStore { ConnectionStringName = "Poll" }.Initialize();
        }

        static IDocumentSession GetDocumentSesssion(IKernel kernel)
        {
            return kernel.Resolve<IDocumentStore>().OpenSession();
        }
    }
}