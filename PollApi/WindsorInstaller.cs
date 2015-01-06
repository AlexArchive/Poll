using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using Raven.Client;
using Raven.Client.Document;
using System.Web.Http;

namespace PollApi
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<ApiController>()
                .WithServiceSelf()
                .LifestylePerWebRequest());

            container.Register(Classes.FromThisAssembly()
                .BasedOn(typeof(AbstractValidator<>))
                .WithServiceFirstInterface()
                .LifestylePerWebRequest());

            container.Register(Component.For<IDocumentStore>()
                .Instance(CreateDocumentStore())
                .LifeStyle
                .Singleton
            );

            container.Register(Component.For<IDocumentSession>()
                .UsingFactoryMethod(GetDocumentSesssion)
                .LifeStyle
                .PerWebRequest);
        }

        private static IDocumentStore CreateDocumentStore()
        {
            return new DocumentStore { ConnectionStringName = "Poll" }.Initialize();
        }

        private static IDocumentSession GetDocumentSesssion(IKernel kernel)
        {
            return kernel.Resolve<IDocumentStore>().OpenSession();
        }
    }
}