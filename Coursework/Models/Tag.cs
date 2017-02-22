using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        
        public virtual ICollection<Instruction> Instructions { get; set; }
        public Tag()
        {
            Instructions = new List<Instruction>();
        }
    }
}