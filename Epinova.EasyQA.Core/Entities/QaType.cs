using System.Collections.Generic;

namespace Epinova.EasyQA.Core.Entities
{
    public class QaType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<CriteriaCategory> CriteriaCategories { get; set; }

        private int LastCategoryId { get; set; }
        private int LastCriteriaId { get; set; }

        public int GenerateNewCategoryId()
        {
            return ++LastCategoryId;
        }

        public int GenerateNewCriteriaId()
        {
            return ++LastCriteriaId;
        }

        public QaType()
        {
            CriteriaCategories = new List<CriteriaCategory>();
        }
    }
}