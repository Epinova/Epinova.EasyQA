using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Core.ServiceInterfaces;
using Epinova.EasyQA.Data.Repositories;

namespace Epinova.EasyQA.Services
{
    public class QaTypeService : IQaTypeService
    {
        private IQaTypeRepository _qaTypeRepository;
        private IQaCategoryRepository _categoryRepository;
        private ICriteriaRepository _criteriaRepository;

        public QaTypeService(IQaTypeRepository qaTypeRepository, ICriteriaRepository criteriaRepository, IQaCategoryRepository categoryRepository)
        {
            if (qaTypeRepository == null)
                throw new NullReferenceException("qaTypeRepository cannot be null!");

            if (criteriaRepository == null)
                throw new NullReferenceException("criteriaRepository cannot be null!");

            if (categoryRepository == null)
                throw new NullReferenceException("categoryRepository cannot be null!");

            _qaTypeRepository = qaTypeRepository;
            _categoryRepository = categoryRepository;
            _criteriaRepository = criteriaRepository;
        }

        public QaTypeService() : this(new QaTypeRepository(), new CriteriaRepository(), new CategoryRepository()) { }

        public QaCriteria CreateQaCriteria(int qaType, int categoryId)
        {
            return _criteriaRepository.CreateQaCriteria(qaType, categoryId);
        }

        public QaCriteria UpdateQaCriteria(int qaType, int criteriaId, string title)
        {
            return _criteriaRepository.UpdateQaCriteria(qaType, criteriaId, title);
        }

        public QaType CreateQaType()
        {
            return _qaTypeRepository.CreateQaType();
        }

        public QaType UpdateQaType(int id, string title)
        {
            return _qaTypeRepository.UpdateQaType(id, title);
        }

        public CriteriaCategory CreateCriteriaCategory(int qaType)
        {
            return _categoryRepository.CreateCriteriaCategory(qaType);
        }

        public CriteriaCategory UpdateCriteriaCategory(int qaTypeId, int categoryId, string title)
        {
            return _categoryRepository.UpdateCriteriaCategory(qaTypeId, categoryId, title);
        }

        public QaType GetQaType(int id)
        {
            return _qaTypeRepository.Get(id);
        }

        public IEnumerable<QaType> GetQaTypes()
        {
            return _qaTypeRepository.GetAll();
        }
    }
}
