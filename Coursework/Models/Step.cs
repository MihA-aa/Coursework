using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Step
    {
        public int Id { get; set; }
        public string StepName { get; set; }
        public int NumberOfStep { get; set; }
        public string PathToImage { get; set; }
        public string Description { get; set; }
        public int? InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }
    }
}