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
            IEnumerable<QaType> types = _session.Query<QaType>();
            return types;
        }

        public QaType Get(int id)
        {
            return _session.Load<QaType>(id);
        }

        public QaType CreateQaType()
        {
            QaType newType = new QaType() { Name = Constants.DefaultQaTypeName };
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