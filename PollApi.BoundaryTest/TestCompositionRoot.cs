using System;
using Raven.Client;
using Raven.Client.Embedded;

namespace Poll.BoundaryTest
{
    public class TestCompositionRoot : CompositionRoot
    {
        protected override IDocumentSession CreateSession()
        {
            return EmbeddableSessionFactory.Create();
        }
    }

    public static class EmbeddableSessionFactory
    {
        private static readonly Lazy<IDocumentStore> _store = new Lazy<IDocumentStore>(DocumentStoreFactory);

        private static IDocumentStore DocumentStoreFactory()
        {
            return new EmbeddableDocumentStore { RunInMemory = true }.Initialize();
        }

        public static IDocumentSession Create()
        {
            return _store.Value.OpenSession();
        }
    }
}