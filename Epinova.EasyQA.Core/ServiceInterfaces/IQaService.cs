using System.Collections.Generic;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Core.ServiceInterfaces
{
    public interface IQaService
    {
        /// <summary>
        /// Creates a new QA instance based on an existing QA type.
        /// </summary>
        /// <returns></returns>
        QaInstance CreateQaInstance(int qaTypeId, string username);

        /// <summary>
        /// Updates a criteria instance with a new status and comment
        /// </summary>
        QaInstanceCriteria UpdateCriteriaInstance(int criteriaId, int qaId, string comment);

        /// <summary>
        /// Updates a criteria instance with a new status
        /// </summary>
        QaInstanceCriteria UpdateCriteriaInstance(int criteriaId, int qaId, InstanceCriteriaStatus status);

        /// <summary>
        /// Updates a QA Instance with a new name
        /// </summary>
        QaInstance UpdateQaInstance(int qaInstanceId, string name);

        /// <summary>
        /// Updates a QA Instance with a publish-state
        /// </summary>
        QaInstance UpdateQaInstance(int qaInstanceId, bool published);

        /// <summary>
        /// Returns all QA instances
        /// </summary>
        IEnumerable<QaInstance> GetAll();

        /// <summary>
        /// Returns all QA instances which a specified user has access to.
        /// </summary>
        IEnumerable<QaInstance> GetAll(string username);

        /// <summary>
        /// Returns the QA instance with the given ID
        /// </summary>
        QaInstance Get(int id);


        /// <summary>
        /// Returns the QA instance with the given ID
        /// </summary>
        QaInstance Get(string username, int id);
    }
}