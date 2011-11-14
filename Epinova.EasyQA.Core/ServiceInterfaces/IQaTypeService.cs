using System.Collections.Generic;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Core.ServiceInterfaces
{
    public interface IQaTypeService
    {
        /// <summary>
        /// Creates a new criteria. Returns the ID of the new criteria.
        /// </summary>
        QaCriteria CreateQaCriteria(int qaType, int categoryId);

        /// <summary>
        /// Updates a criteria with new text.
        /// </summary>
        /// <param name="criteriaId"></param>
        /// <param name="title"></param>
        /// <param name="qaType"></param>
        QaCriteria UpdateQaCriteria(int qaType, int criteriaId, string title);

        /// <summary>
        /// Creates a new QA type
        /// </summary>
        /// <returns>The ID of the newly created QA type</returns>
        QaType CreateQaType();

        /// <summary>
        /// Updates a QA type
        /// </summary>
        /// <param name="id">The ID of the QA type to update</param>
        /// <param name="title">The new title</param>
        QaType UpdateQaType(int id, string title);

        /// <summary>
        /// Creates a new criteria category. Returns the ID of the new category.
        /// </summary>
        /// <param name="qaType">The ID of the QA type to which the category will belong</param>
        /// <returns></returns>
        CriteriaCategory CreateCriteriaCategory(int qaType);

        /// <summary>
        /// Updates a criteria category.
        /// </summary>
        /// <param name="title">The new category title</param>
        /// <param name="categoryId">The ID of the category to update</param>
        /// <param name="qaTypeId">The QA type to which the category belongs</param>
        /// <returns></returns>
        CriteriaCategory UpdateCriteriaCategory(int qaTypeId, int categoryId, string title);

        /// <summary>
        /// Returns the QA type with the specified ID.
        /// </summary>
        QaType GetQaType(int id);

        /// <summary>
        /// Returns all QA-types
        /// </summary>
        IEnumerable<QaType> GetQaTypes();
    }
}