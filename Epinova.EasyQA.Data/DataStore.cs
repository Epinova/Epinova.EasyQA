using System.Configuration;
using Raven.Client.Document;

namespace Epinova.EasyQA.Data
{
    public static class DataStore
    {
        private static DocumentStore _store;
        public static DocumentStore Store
        {
            get
            {
                if (_store == null)
                {
                    _store = new DocumentStore() { ConnectionStringName = "EasyQaRaven" };
                    _store.DefaultDatabase = ConfigurationManager.AppSettings["DefaultDb"];
                    _store.Initialize();
                    
                }
                return _store;
            }
        }
    }
}