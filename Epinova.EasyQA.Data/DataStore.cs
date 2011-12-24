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
<<<<<<< HEAD
                    _store.DefaultDatabase = ConfigurationManager.AppSettings["DefaultDb"];
                    _store.Initialize();
                    
=======
                    _store.Initialize();
>>>>>>> 0304d19823908e6f8d019f9dfdc9d2d549b6dd57
                }
                return _store;
            }
        }
    }
}