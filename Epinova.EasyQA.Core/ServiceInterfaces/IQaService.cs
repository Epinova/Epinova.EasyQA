using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Core.ServiceInterfaces
{
    public interface IQaService
    {
        /// <summary>
        /// Updates a criteria instance with a new status and comment
        /// </summary>
        QaInstanceCriteria UpdateCriteriaInstance(int criteriaId, string comment);

        /// <summary>
        /// Updates a criteria instance with a new status
        /// </summary>
        QaInstanceCriteria UpdateCriteriaInstance(int criteriaId, InstanceCriteriaStatus status);

        /// <summary>
        /// Updates a QA Instance with a new name
        /// </summary>
        QaInstance UpdateQaInstance(int qaInstanceId, string name);

        /// <summary>
        /// Updates a QA Instance with a publish-state
        /// </summary>
        QaInstance UpdateQaInstance(int qaInstanceId, bool published);
    }
}