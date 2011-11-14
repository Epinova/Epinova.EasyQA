using System;
using System.Collections.Generic;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Core.ServiceInterfaces;

namespace Epinova.EasyQA.Services
{
    public class QaTypeService : IQaTypeService
    {
        private IQaTypeRepository QaTypeRepository { get; set; }
        private ICriteriaRepository CriteriaRepository { get; set; }
        private IChangeLogRepository ChangeLogRepository { get; set; }

        public QaTypeService(IQaTypeRepository qaTypeRepository, ICriteriaRepository criteriaRepository, IChangeLogRepository changelogRepository)
        {
            QaTypeRepository = qaTypeRepository;
            CriteriaRepository = criteriaRepository;
            ChangeLogRepository = changelogRepository;
        }

        public int CreateQaCriteria(int categoryId)
        {
            int newQaId = CriteriaRepository.CreateQaCriteria(categoryId);
            // Change change = new Change() { CriteriaId = newQaId, OldValue = string.Empty, NewValue = string.Empty };
            // ChangeLogRepository.Save(change);
            return newQaId;
        }

        public void UpdateQaCriteria(int criteriaId, string title)
        {
            CriteriaRepository.UpdateQaCriteria(criteriaId, title);
        }

        public int CreateQaType()
        {
            return QaTypeRepository.CreateQaType();
        }

        public void UpdateQaType(int id, string title)
        {
            QaTypeRepository.UpdateQaType(id, title);
        }

        public int CreateCriteriaCategory(int qaType)
        {
            return QaTypeRepository.CreateCriteriaCategory(qaType);
        }

        public void UpdateCriteriaCategory(int id, string title)
        {
            QaTypeRepository.UpdateCriteriaCategory(id, title);
        }

        public QaType GetQaType(int id)
        {
            return QaTypeRepository.Get(id);
        }

        public IList<QaType> GetQaTypes()
        {
            return QaTypeRepository.GetAll();
        }
    }
}