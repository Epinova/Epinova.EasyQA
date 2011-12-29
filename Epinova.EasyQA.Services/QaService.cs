using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
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

        public QaInstance CreateQaInstance(int qaTypeId, string username)
        {
            QaInstance newQaInstance = new QaInstance(_qaTypeRepository.Get(qaTypeId), username);
            
            _qaInstanceRepository.SaveQaInstance(newQaInstance);
            return newQaInstance;
        }

        public QaInstanceCriteria UpdateCriteriaComment(int criteriaId, int qaId, string comment)
        {
            QaInstance qa = _qaInstanceRepository.Get(qaId);

            QaInstanceCriteria criteria;
            foreach (QaInstanceCategory cat in qa.Categories)
            {
                criteria = cat.Criterias.Where(x => x.Id == criteriaId).FirstOrDefault();
                if (criteria != null) { 
                    criteria.Comment = comment;
                    _qaInstanceRepository.SaveQaInstance(qa);
                    return criteria;
                }
            }

            throw new NullReferenceException("No criteria with ID " + criteriaId + " in QA Instance #" + qaId);
        }

        public QaInstanceCriteria UpdateCriteriaStatus(int criteriaId, int qaId, InstanceCriteriaStatus status)
        {
            QaInstance qa = _qaInstanceRepository.Get(qaId);

            QaInstanceCriteria criteria;
            foreach (QaInstanceCategory cat in qa.Categories)
            {
                criteria = cat.Criterias.Where(x => x.Id == criteriaId).FirstOrDefault();
                if (criteria != null) { 
                    criteria.Status = status;
                    _qaInstanceRepository.SaveQaInstance(qa);
                    return criteria;
                }
            }
            throw new NullReferenceException("No criteria with ID " + criteriaId + " in QA Instance #" + qaId);
        }

        public QaInstanceCriteria UpdateCriteriaCorrected(int criteriaId, int qaId, bool corrected)
        {
            QaInstance qa = _qaInstanceRepository.Get(qaId);

            QaInstanceCriteria criteria;
            foreach (QaInstanceCategory cat in qa.Categories)
            {
                criteria = cat.Criterias.Where(x => x.Id == criteriaId).FirstOrDefault();
                if (criteria != null)
                {
                    criteria.Corrected = corrected;
                    _qaInstanceRepository.SaveQaInstance(qa);
                    return criteria;
                }
            }
            throw new NullReferenceException("No criteria with ID " + criteriaId + " in QA Instance #" + qaId);
        }
        
        public QaInstance UpdateQaName(int qaInstanceId, string name)
        {
            QaInstance qa = _qaInstanceRepository.Get(qaInstanceId);
            qa.Name = name;
            return _qaInstanceRepository.SaveQaInstance(qa);
        }

        public QaInstance UpdateQaPublished(int qaInstanceId, bool published)
        {
            QaInstance qa = _qaInstanceRepository.Get(qaInstanceId);
            qa.Published = published;
            qa.PublishedDate = DateTime.Now;
            return _qaInstanceRepository.SaveQaInstance(qa);
        }

        public QaInstance UpdateQaSummary(int qaInstanceId, string summary)
        {
            QaInstance qa = _qaInstanceRepository.Get(qaInstanceId);
            qa.Summary = summary;
            return _qaInstanceRepository.SaveQaInstance(qa);
        }

        public QaInstance UpdateQaMisc(int qaInstanceId, string misc)
        {
            QaInstance qa = _qaInstanceRepository.Get(qaInstanceId);
            qa.Miscellaneous = misc;
            return _qaInstanceRepository.SaveQaInstance(qa);
        }

        public QaInstance UpdateQaProjectMembers(int qaInstanceId, List<string> projectMembers)
        {
            QaInstance qa = _qaInstanceRepository.Get(qaInstanceId);
            qa.ProjectMembers = projectMembers;
            return _qaInstanceRepository.SaveQaInstance(qa);
        }

        public QaInstance UpdateQaPresentAtReview(int qaInstanceId, string presentAtReview)
        {
            QaInstance qa = _qaInstanceRepository.Get(qaInstanceId);
            qa.PresentAtReview = presentAtReview;
            return _qaInstanceRepository.SaveQaInstance(qa);
        }

        public IEnumerable<QaInstance> GetAll()
        {
            return _qaInstanceRepository.GetAll();
        }

        public IEnumerable<QaInstance> GetAll(string username)
        {
            IEnumerable<QaInstance> qas = _qaInstanceRepository.GetAll();
            IEnumerable<QaInstance> qasToReturn = qas.Where(qa => qa.Published || qa.User == username);
            return qasToReturn;
        }

        public QaInstance Get(string username, int id)
        {
            QaInstance qa = _qaInstanceRepository.Get(id);
            if (qa.Published)
                return qa;

            if (qa.User != username)
                throw new AccessViolationException("No access!");
            return qa;
        }

        public QaInstance Get(int id)
        {
            return _qaInstanceRepository.Get(id);
        }
    }
}