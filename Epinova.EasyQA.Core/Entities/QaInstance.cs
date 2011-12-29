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
        public string User { get; set; }
        public List<string> ProjectMembers { get; set; }
        public string PresentAtReview { get; set; }
        public string Miscellaneous { get; set; }
        public string Summary { get; set; }

        public QaInstance() {}
        public QaInstance(QaType qaType, string username)
        {
            QaTypeName = qaType.Name;
            Name = Constants.DefaultQaInstanceName;
			Published = false;
            User = username;
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
                                                      Status = InstanceCriteriaStatus.NotSet
                                                  });
                }
                Categories.Add(instanceCategory);
            }
        }
    }
}
