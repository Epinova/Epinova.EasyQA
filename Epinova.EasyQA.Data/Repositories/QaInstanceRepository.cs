using System;
using System.Collections.Generic;
using Epinova.EasyQA.Core;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Data.Base;

namespace Epinova.EasyQA.Data.Repositories
{
    public class QaInstanceRepository : RepositoryBase, IQaInstanceRepository
    {
        public IEnumerable<QaInstance> GetAll()
        {
            return _session.Query<QaInstance>();
        }

        public QaInstance Get(int id)
        {
            return _session.Load<QaInstance>(id);
        }

        public QaInstance SaveQaInstance(QaInstance qa)
        {
            _session.Store(qa);
            _session.SaveChanges();
            return qa;
        }
    }
}