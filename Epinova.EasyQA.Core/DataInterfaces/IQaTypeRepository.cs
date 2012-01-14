using System;
using System.Collections.Generic;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Core.DataInterfaces
{
    public interface IQaTypeRepository
    {
        /// <summary>
        /// Returns all QA types
        /// </summary>
        IEnumerable<QaType> GetAll();

        /// <summary>
        /// Returns the QA type with the specified ID.
        /// </summary>
        QaType Get(int id);

        /// <summary>
        /// Creates a new QA type
        /// </summary>
        /// <returns>The ID of the saved QA type.</returns>
        QaType CreateQaType(string name);

        /// <summary>
        /// Updates a QA-type with a new title
        /// </summary>
        /// <param name="qaTypeId">The ID of the QA-type</param>
        /// <param name="title">The new title</param>
        QaType UpdateQaType(int qaTypeId, string title);
    }
}