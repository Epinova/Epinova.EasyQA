using System;
using System.Collections.Generic;

namespace Epinova.EasyQA.Core.Entities
{
    public class QaInstance
    {
        public int Id { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Name { get; set; }
        public QaType QaType { get; set;}    
        public List<QaInstanceCriteria> Criterias { get; set; }
        public bool Published { get; set; }
        public string User { get; set; } // Skal inneholde bruker som opprettet den. Kun denne personen kan se den om den ikke enda er publisert.

        public QaInstance(QaType qaType)
        {
            int idCounter = 0;

            foreach (CriteriaCategory category in qaType.CriteriaCategories)
            {
                foreach (QaCriteria criteria in category.Criterias)
                {
                    Criterias.Add(new QaInstanceCriteria()
                                      {
                                          Id = ++idCounter,
                                          CriteriaId = criteria.Id
                                      });
                }
            }
        }
    }
}
