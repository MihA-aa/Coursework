using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }
        public bool RatingScore { get; set; }
    }
}