using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Core.DataInterfaces
{
    public interface IQaCategoryRepository
    {
        /// <summary>
        /// Saves a new criteria category to a QA type.
        /// </summary>
        /// <param name="qaType">The ID of the QA type to save the criteria to.</param>
        CriteriaCategory CreateCriteriaCategory(int qaType, string text);

        /// <summary>
        /// Updates 
        /// </summary>
        /// <param name="categoryId">The ID of the category to update</param>
        /// <param name="title">The title of the category</param>
        /// <param name="qaType">The QA type to which the category belongs</param>
        CriteriaCategory UpdateCriteriaCategory(int qaType, int categoryId, string title);
    }
}