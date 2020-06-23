using System;
using System.Collections.Generic;
using System.Text;

namespace ContentsLimit.Business.Models
{
    /// <summary>
    /// Class which reflects our persistent database schema. Use this class when inserting new record.
    /// </summary>
    public class ContentItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }
}
