namespace Epinova.EasyQA.Core.Entities
{
    public class Change
    {
        public int Id { get; set; }
        public int CriteriaId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}