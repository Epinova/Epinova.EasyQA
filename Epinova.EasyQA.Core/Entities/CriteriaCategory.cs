using System.Collections.Generic;

namespace Epinova.EasyQA.Core.Entities
{
    public class CriteriaCategory
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IList<QaCriteria> Criterias { get; set;}
        public int SortOrder { get; set; }

        public CriteriaCategory()
        {
            Criterias = new List<QaCriteria>();
        }
    }
}