using System;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Tests.Mocks
{
    public class CriteriaRepository : ICriteriaRepository
    {
        public QaCriteria CreateQaCriteria(int qaType, int criteriaCategory)
        {
            throw new NotImplementedException();
        }

        public QaCriteria UpdateQaCriteria(int qaTypeId, int criteriaId, string text)
        {
            throw new NotImplementedException();
        }
    }
}