using System;
using System.Collections.Generic;
using System.Linq;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Tests.Mocks
{
    public class QaTypeRepositoryMock : IQaTypeRepository
    {
        private List<QaType> qaTypes;

        public QaTypeRepositoryMock()
        {
            qaTypes = new List<QaType>();
        }

        public IEnumerable<QaType> GetAll()
        {
            return qaTypes;
        }

        public QaType Get(int id)
        {
            return qaTypes.Where(x => x.Id == id).SingleOrDefault();
        }

        public QaType CreateQaType()
        {
            QaType qaType = new QaType();
            qaTypes.Add(qaType);
            return qaType;
        }

        public QaType UpdateQaType(int qaTypeId, string title)
        {
            QaType qatype = qaTypes.Where(x => x.Id == qaTypeId).SingleOrDefault();
            qatype.Name = title;
            return qatype;
        }
    }
}