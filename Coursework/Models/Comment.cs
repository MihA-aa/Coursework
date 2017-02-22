using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Author { get; set; }
        public int? InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }
        public string Contetnt { get; set; }
    }
}