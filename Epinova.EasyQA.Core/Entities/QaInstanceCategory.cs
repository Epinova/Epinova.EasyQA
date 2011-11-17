using System.Collections.Generic;

namespace Epinova.EasyQA.Core.Entities
{
    public class QaInstanceCategory
    {
        public string Text { get; set; }
        public List<QaInstanceCriteria> Criterias { get; set; }

        public QaInstanceCategory(string text)
        {
            Text = text;
            Criterias = new List<QaInstanceCriteria>();
        }
    }
}