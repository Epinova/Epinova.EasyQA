using System;
using System.Collections.Generic;
using System.Linq;
using Epinova.EasyQA.Core;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Data.Base;

namespace Epinova.EasyQA.Data.Repositories
{
    public class CriteriaRepository : RepositoryBase, ICriteriaRepository
    {
        public QaCriteria CreateQaCriteria(int qaType, int criteriaCategory)
        {
            QaType qaTypeToUpdate = _session.Load<QaType>(qaType);
            CriteriaCategory category = qaTypeToUpdate.CriteriaCategories.Where(x => x.Id == criteriaCategory).First();
            QaCriteria newCriteria = new QaCriteria(qaTypeToUpdate.GenerateNewCriteriaId(), Constants.DefaultCriteriaName, DateTime.Now);
            category.Criterias.Add(newCriteria);
            _session.Store(qaTypeToUpdate);
            _session.SaveChanges();
            return newCriteria;
        }

        public QaCriteria UpdateQaCriteria(int qaTypeId, int criteriaId, string text)
        {
            QaType qaTypeToUpdate = _session.Load<QaType>(qaTypeId);
            QaCriteria criteria = qaTypeToUpdate.CriteriaCategories.Select(cat => cat.Criterias.Where(x => x.Id == criteriaId)).Single().Single();
            criteria.Text = text;
            _session.Store(qaTypeToUpdate);
            _session.SaveChanges();
            return criteria;
        }
    }
}