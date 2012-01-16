using System;

namespace Epinova.EasyQA.Core.Entities
{
    public class QaInstanceCriteria
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public InstanceCriteriaStatus Status { get; set; }
        public string Comment { get; set; }
        public ResolvedType ResolvedAs { get; set; }
    }
}