using System;
using System.Collections.Generic;

namespace Epinova.EasyQA.Core.Entities
{
    public class QaInstance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public List<QaInstanceCriteria> Criterias { get; set; }
        public QaType QaType { get; set; }
        public bool Published { get; set; }
        public string User { get; set; } // Skal inneholde bruker som opprettet den. Kun denne personen kan se den om den ikke enda er publisert.
    }
}
