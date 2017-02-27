using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Step
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название не может быть пустым")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Название должно содержать от 3 до 40 символов")]
        [Display(Name = "Название")]
        public string StepName { get; set; }
        public int NumberOfStep { get; set; }

        [Display(Name = "Изображение")]
        public string PathToImage { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
        public int? InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }
    }
}