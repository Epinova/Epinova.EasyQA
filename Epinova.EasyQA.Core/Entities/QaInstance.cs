using System;
using System.Collections.Generic;

namespace Epinova.EasyQA.Core.Entities
{
    public class QaInstance
    {
        public int Id { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Name { get; set; }
        public string QaTypeName { get; set; }
        public List<QaInstanceCategory> Categories { get; set; }
        public bool Published { get; set; }
        public string User { get; set; } // Skal inneholde bruker som opprettet den. Kun denne personen kan se den om den ikke enda er publisert.

        public QaInstance() {}
        public QaInstance(QaType qaType)
        {
            QaTypeName = qaType.Name;

            int idCounter = 0;
            Categories = new List<QaInstanceCategory>();

            foreach (CriteriaCategory category in qaType.CriteriaCategories)
            {
                QaInstanceCategory instanceCategory = new QaInstanceCategory(category.Text);
                foreach (QaCriteria criteria in category.Criterias)
                {
                    instanceCategory.Criterias.Add(new QaInstanceCriteria()
                                                  {
                                                      Comment = string.Empty,
                                                      Corrected = false,
                                                      Id = ++idCounter,
                                                      Text = criteria.Text,
                                                  });
                }
                Categories.Add(instanceCategory);
            }
        }
    }
}
