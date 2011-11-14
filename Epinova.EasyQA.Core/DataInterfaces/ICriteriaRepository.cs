using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Core.DataInterfaces
{
    public interface ICriteriaRepository
    {
        /// <summary>
        /// Creates a new criteria
        /// </summary>
        /// <param name="qaType">The QA type to which the new criteria will belong</param>
        /// <param name="criteriaCategory">The category to which the new criteria will belong</param>
        /// <returns>The ID of the newly created criteria</returns>
        QaCriteria CreateQaCriteria(int qaType, int criteriaCategory);

        /// <summary>
        /// Updates a criteria
        /// </summary>
        /// <param name="criteriaId">The ID of the criteria to update</param>
        /// <param name="qaTypeId">The QA type to which the criteria belongs</param>
        /// <param name="text">The new criteria text</param>
        QaCriteria UpdateQaCriteria(int qaTypeId, int criteriaId, string text);
    }
}