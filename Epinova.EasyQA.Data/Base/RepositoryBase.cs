using Epinova.EasyQA.Core.DataInterfaces;
using Raven.Client;
using Raven.Client.Document;

namespace Epinova.EasyQA.Data.Base
{
    public class RepositoryBase
    {
        protected DocumentStore _store;
        protected IDocumentSession _session;

        public RepositoryBase()
        {
            _store = DataStore.Store;
            _session = _store.OpenSession();
        }
    }
}