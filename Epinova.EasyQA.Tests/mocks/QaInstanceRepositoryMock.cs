using System;
using System.Collections.Generic;
using System.Linq;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Tests.Mocks
{
    public class QaInstanceRepositoryMock : IQaInstanceRepository
    {
        private List<QaInstance> qaInstances;

        public QaInstanceRepositoryMock()
        {
            qaInstances = new List<QaInstance>();
        }

        public QaInstance Get(int id)
        {
            return qaInstances.Where(x => x.Id == id).SingleOrDefault();
        }

        public IEnumerable<QaInstance> GetAll()
        {
            return qaInstances;
        }

        public QaInstance SaveQaInstance(QaInstance qa)
        {
            return qa;
        }
    }
}