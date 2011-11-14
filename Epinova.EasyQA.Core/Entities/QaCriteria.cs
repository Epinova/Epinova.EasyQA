using System;

namespace Epinova.EasyQA.Core.Entities
{
    public class QaCriteria
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }
        public int SortOrder { get; set; }

        public QaCriteria(int id, string text, DateTime dateAdded)
        {
            Id = id;
            Text = text;
            DateAdded = DateAdded;
        }
    }
}