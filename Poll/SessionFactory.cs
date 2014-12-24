﻿using System;
using Raven.Client;
using Raven.Client.Document;

namespace Poll
{
    public static class SessionFactory
    {
        private static readonly Lazy<IDocumentStore> _store = new Lazy<IDocumentStore>(DocumentStoreFactory);

        private static IDocumentStore DocumentStoreFactory()
        {
            return new DocumentStore { ConnectionStringName = "Poll" }.Initialize();
        }

        public static IDocumentSession Create()
        {
            return _store.Value.OpenSession();
        }
    }
}