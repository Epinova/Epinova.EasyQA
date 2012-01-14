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
        public QaInstance(QaType qaType, string username, string name)
        {
            QaTypeName = qaType.Name;
            Name = "uten navn";
			Published = false;
            User = username;
            int idCounter = 0;
            Categories = new List<QaInstanceCategory>();
            ProjectMembers = new List<string>();

            foreach (CriteriaCategory category in qaType.CriteriaCategories)
            {
                QaInstanceCategory instanceCategory = new QaInstanceCategory(category.Text);
                foreach (QaCriteria criteria in category.Criterias)
                {
                    instanceCategory.Criterias.Add(new QaInstanceCriteria()
                                                  {
                                                      Comment = string.Empty,
                                                      Fixed = false,
                                                      Id = ++idCounter,
                                                      Text = criteria.Text,
                                                      Status = InstanceCriteriaStatus.NotSet
                                                  });
                }
                Categories.Add(instanceCategory);
            }
        }

        private int _score = -1;
        /// <summary>
        /// Gets the score (100 is max) for this QA, and basically gives a percentage of right vs wrong. Ignores criterias that has not been set as right/wrong/needs explanation.
        /// </summary>
        public int Score
        {
            get
            {
                if(_score > -1)
                    return _score;

                int ok = 0;
                int totalCount = 0;
                int needsExplanation = 0;

                foreach (QaInstanceCategory category in Categories)
                {
                    foreach (QaInstanceCriteria criteria in category.Criterias)
                    {
                        totalCount++;
                        switch(criteria.Status)
                        {
                            case InstanceCriteriaStatus.Ok:
                                ok++;
                                break;
                            case InstanceCriteriaStatus.NotSet:
                                totalCount--;
                                break;
                            case InstanceCriteriaStatus.NeedsExplanation:
                                needsExplanation++;
                                break;
                        }
                    }
                }
                if (totalCount == 0)
                    return 0;



                _score = (int)(((ok + ((double)needsExplanation / 2)) / totalCount) * 100);
                return _score;
            }
        }
    }
}
