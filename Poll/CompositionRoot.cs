using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Raven.Client;
using Raven.Client.Document;

namespace Poll
{
    public class CompositionRoot : IHttpControllerActivator
    {
        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var session = StoreProvider.Value.OpenSession();
            request.DisposeRequestResources();
            request.RegisterForDispose(new Release(() =>
            {
                session.SaveChanges();
                session.Dispose();
            }));
            return new PollController(session);
        }

        private static readonly Lazy<IDocumentStore> StoreProvider =
            new Lazy<IDocumentStore>(CreateDocumentStore);

        private static IDocumentStore CreateDocumentStore()
        {
            return new DocumentStore { ConnectionStringName = "Poll" }.Initialize();
        }

        private class Release : IDisposable
        {
            private readonly Action _release;

            public Release(Action release)
            {
                _release = release;
            }

            public void Dispose()
            {
                _release.Invoke();
            }
        }
    }
}