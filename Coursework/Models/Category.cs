using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string СategoryName { get; set; }

        public virtual ICollection<Instruction> Instructions { get; set; }
        public Category()
        {
            Instructions = new List<Instruction>();
        }
    }
}