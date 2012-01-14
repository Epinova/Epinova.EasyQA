using System;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Tests.Mocks
{
    public class CategoryRepositoryMock : IQaCategoryRepository
    {
        public CriteriaCategory CreateCriteriaCategory(int qaType, string text)
        {
            throw new NotImplementedException();
        }

        public CriteriaCategory UpdateCriteriaCategory(int qaType, int categoryId, string title)
        {
            throw new NotImplementedException();
        }
    }
}