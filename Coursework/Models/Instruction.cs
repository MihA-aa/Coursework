using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Instruction
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Название не может быть пустым")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Название должно содержать от 3 до 40 символов")]
        [Display(Name = "Название")]
        public string InstructionName { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [RegularExpression(@"(?:https?:\/\/)?(?:www\.)?youtu\.?be(?:\.com)?\/?.*(?:watch|embed)?(?:.*v=|v\/|\/)([\w\-_]+)\&?", ErrorMessage = "Некорректная ссылка")]
        [Display(Name = "Ссылка на видео")]
        public string LinkToVideo { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime DateOfCreation{ get; set; }


        [Display(Name = "Шаги")]
        public virtual ICollection<Step> Steps { get; set; }

        [Display(Name = "Комментарии")]
        public virtual ICollection<Comment> Comments { get; set; }

        [Display(Name = "Теги")]
        public virtual ICollection<Tag> Tags { get; set; }

        [Display(Name = "Рейтинг")]
        public virtual ICollection<Rating> Ratings { get; set; }
        
        [Display(Name = "Категории")]
        public virtual ICollection<Category> Categories { get; set; }

        public Instruction()
        {
            Steps = new List<Step>();
            Tags = new List<Tag>();
            Comments = new List<Comment>();
            Ratings = new List<Rating>();
            Categories = new List<Category>();
        }
        
    }
}