using System;
using System.Collections.Generic;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Core.DataInterfaces
{
    public interface IChangeLogRepository
    {
        /// <summary>
        /// Returns the entire change log
        /// </summary>
        /// <returns></returns>
        IList<Change> GetAll();

        /// <summary>
        /// Returns all changes for a given QA type
        /// </summary>
        /// <param name="qaTypeId">The ID of the QA type to retrieve changes for</param>
        /// <returns></returns>
        IList<Change> GetForQaType(int qaTypeId);

        /// <summary>
        /// Returns all changes for a given criteria
        /// </summary>
        /// <param name="criteriaId">The ID of the criteria to retrieve changes for</param>
        IList<Change> GetForCriteria(int criteriaId);

        Change Get(int id);

        /// <summary>
        /// Saves a new change.
        /// </summary>
        void Save(Change change);
    }
}