using System;

namespace Epinova.EasyQA.Core.Entities
{
    public class QaCriteria
    {
        public int Id { get; set; }
        public string Text { get; set; }
        
        public QaCriteria() {} 
        public QaCriteria(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}