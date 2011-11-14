namespace Epinova.EasyQA.Core.Entities
{
    public class QaInstanceCriteria
    {
        public QaCriteria Criteria { get; set; }
        public InstanceCriteriaStatus Status { get; set; }
        public string Comment { get; set; }
        public bool Corrected { get; set; }
    }
}