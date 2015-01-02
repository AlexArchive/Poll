using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Raven.Client;

namespace PollApi
{
    public class CompositionRoot : IHttpControllerActivator
    {
        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var session = CreateSession();
            request.RegisterForDispose(new SessionRelease(session));

            return (IHttpController) Activator.CreateInstance(controllerType, session);
        }

        private class SessionRelease : IDisposable
        {
            private readonly IDocumentSession _session;

            public SessionRelease(IDocumentSession session)
            {
                _session = session;
            }

            public void Dispose()
            {
                _session.SaveChanges();
                _session.Dispose();
            }
        }

        protected virtual IDocumentSession CreateSession()
        {
            return SessionFactory.Create();
        }
    }
}