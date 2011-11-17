using System;
using System.Collections.Generic;
using Epinova.EasyQA.Core;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Core.ServiceInterfaces;
using Epinova.EasyQA.Data.Repositories;

namespace Epinova.EasyQA.Services
{
    public class QaService : IQaService
    {
        private IQaTypeRepository _qaTypeRepository;
        private IQaInstanceRepository _qaInstanceRepository;

        public QaService() : this(new QaTypeRepository(), new QaInstanceRepository()) { }

        public QaService(IQaTypeRepository qaTypeRepository, IQaInstanceRepository qaInstanceRepository)
        {
            if (qaTypeRepository == null)
                throw new NullReferenceException("qaTypeRepository cannot be null!");

            if (qaInstanceRepository == null)
                throw new NullReferenceException("qaInstanceRepository cannot be null!");

           
            _qaTypeRepository = qaTypeRepository;
            _qaInstanceRepository = qaInstanceRepository;
        }

        public QaInstance CreateQaInstance(int qaTypeId)
        {
            QaInstance newQaInstance = new QaInstance(_qaTypeRepository.Get(qaTypeId));
            
            _qaInstanceRepository.SaveQaInstance(newQaInstance);
            return newQaInstance;
        }

        public QaInstanceCriteria UpdateCriteriaInstance(int criteriaId, string comment)
        {
            throw new NotImplementedException();
        }

        public QaInstanceCriteria UpdateCriteriaInstance(int criteriaId, InstanceCriteriaStatus status)
        {
            throw new NotImplementedException();
        }

        public QaInstance UpdateQaInstance(int qaInstanceId, string name)
        {
            throw new NotImplementedException();
        }

        public QaInstance UpdateQaInstance(int qaInstanceId, bool published)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QaInstance> GetAll()
        {
            return _qaInstanceRepository.GetAll();
        }

        public QaInstance Get(int id)
        {
            return _qaInstanceRepository.Get(id);
        }
    }
}