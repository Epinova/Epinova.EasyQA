using System;
using System.Collections.Generic;
using System.Linq;
using Epinova.EasyQA.Core;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Data.Base;
using Raven.Client;
using Raven.Client.Document;

namespace Epinova.EasyQA.Data.Repositories
{
    public class QaTypeRepository : RepositoryBase, IQaTypeRepository
    {
        public IEnumerable<QaType> GetAll()
        {
            return _session.Query<QaType>();
        }

        public QaType Get(int id)
        {
            return _session.Load<QaType>(id);
        }

        public QaType CreateQaType(string name)
        {
            QaType newType = new QaType() { Name = name };
            _session.Store(newType);
            _session.SaveChanges();
            return newType;
        }

        public QaType UpdateQaType(int qaTypeId, string title)
        {
            QaType qaTypeToUpdate = Get(qaTypeId);
            qaTypeToUpdate.Name = title;
            _session.Store(qaTypeToUpdate);
            _session.SaveChanges();
            return qaTypeToUpdate;
        }

        

    }
}