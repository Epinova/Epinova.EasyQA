using System;
using System.Linq;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Data.Base;

namespace Epinova.EasyQA.Data.Repositories
{
    public class CategoryRepository : RepositoryBase, IQaCategoryRepository
    {
        public CriteriaCategory CreateCriteriaCategory(int qaType)
        {
            QaType qaTypeToAddCatTo = _session.Load<QaType>(qaType);
            int newCategoryId = qaTypeToAddCatTo.GenerateNewCategoryId();
            CriteriaCategory category = new CriteriaCategory() { Id = newCategoryId };
            qaTypeToAddCatTo.CriteriaCategories.Add(category);

            _session.Store(qaTypeToAddCatTo);
            _session.SaveChanges();
            return category;
        }

        public CriteriaCategory UpdateCriteriaCategory(int qaType, int categoryId, string title)
        {
            QaType qaTypeToUpdate = _session.Load<QaType>(qaType);
            CriteriaCategory category = qaTypeToUpdate.CriteriaCategories.Where(x => x.Id == categoryId).First();
            if (category == null)
                throw new NullReferenceException("No category with ID " + categoryId + " in QA type " + qaType);

            category.Text = title;
            _session.Store(qaTypeToUpdate);
            _session.SaveChanges();
            return category;
        }
    }
}