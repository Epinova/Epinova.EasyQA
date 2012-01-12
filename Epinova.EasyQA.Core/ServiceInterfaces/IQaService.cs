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
        QaInstanceCriteria UpdateCriteriaComment(int criteriaId, int qaId, string comment);

        /// <summary>
        /// Updates a criteria instance with a new status
        /// </summary>
        QaInstanceCriteria UpdateCriteriaStatus(int criteriaId, int qaId, InstanceCriteriaStatus status);

        /// <summary>
        /// Updates a criterias fixed state 
        /// </summary>
        QaInstanceCriteria ToggleCriteriaFixed(int criteriaId, int qaId);

        /// <summary>
        /// Updates a QA Instance with a new name
        /// </summary>
        QaInstance UpdateQaName(int qaInstanceId, string name);

        /// <summary>
        /// Updates a QA Instance with a publish-state
        /// </summary>
        QaInstance UpdateQaPublished(int qaInstanceId, bool published);


        /// <summary>
        /// Updates the summary of the QA
        /// </summary>
        QaInstance UpdateQaSummary(int qaInstanceId, string summary);

        /// <summary>
        /// Updates the Miscellaneous-field of the QA
        /// </summary>
        QaInstance UpdateQaMisc(int qaInstanceId, string misc);

        /// <summary>
        /// Adds a new project member to the QA
        /// </summary>
        QaInstance AddQaProjectMember(int qaInstanceId, string projectMember);

        /// <summary>
        /// Removes a project member from the QA
        /// </summary>
        QaInstance RemoveQaProjectMember(int qaInstanceId, string projectMember);
        
        /// <summary>
        /// Updates who were present at the QA review
        /// </summary>
        QaInstance UpdateQaPresentAtReview(int qaInstanceId, string presentAtReview);

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