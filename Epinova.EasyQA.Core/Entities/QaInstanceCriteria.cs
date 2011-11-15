namespace Epinova.EasyQA.Core.Entities
{
    public class QaInstanceCriteria
    {
        public int Id { get; set; }
        public int CriteriaId { get; set; }
        public InstanceCriteriaStatus Status { get; set; }
        public string Comment { get; set; }
        public bool Corrected { get; set; }
    }
}