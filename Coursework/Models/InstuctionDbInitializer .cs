using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class InstuctionDbInitializer : DropCreateDatabaseAlways<PostEntities>
    {
        protected override void Seed(PostEntities context)
        {
            Instruction s1 = new Instruction
            {
                Id = 1,
                UserId = "179738bc-b1cd-45cb-a77a-c72bf94a4e42",
                InstructionName = "Приготовление взрывной бомбы",
                Author = "Miha_aa123",
                LinkToVideo = "https://www.youtube.com/watch?v=ET838We_UvE",
                CategoryId = 1,
                NumberOfLikes = 53,
                DateOfCreation = DateTime.Now
            };
            Instruction s2 = new Instruction {
                Id = 2,
                UserId = "179738bc-b1cd-45cb-a77a-c72bf94a4e42",
                InstructionName = "Инструкция по взрыву банкомата",
                Author = "Miha_aa123",
                LinkToVideo = "https://www.youtube.com/watch?v=ET838We_UvE",
                CategoryId = 2,
                NumberOfLikes = 99,
                DateOfCreation = DateTime.Now.AddMonths(-1)
        };
            Instruction s3 = new Instruction
            {
                Id = 3,
                UserId = "ba47eb41-7d0a-4012-8023-4418ced312c3",
                InstructionName = "Создание кошелька webmoney",
                Author = "SomeUser",
                CategoryId = 3,
                NumberOfLikes = 11,
                DateOfCreation = DateTime.Now.AddYears(-1)
        };

            Instruction s4 = new Instruction
            {
                Id = 4,
                UserId = "179738bc-b1cd-45cb-a77a-c72bf94a4e42",
                InstructionName = "Приготовление борща",
                Author = "Miha_aa123",
                LinkToVideo = "https://www.youtube.com/watch?v=ET838We_UvE",
                CategoryId = 4,
                NumberOfLikes = 0,
                DateOfCreation = DateTime.Now.AddDays(-7)
        };

            context.Instructions.Add(s1);
            context.Instructions.Add(s2);
            context.Instructions.Add(s3);
            context.Instructions.Add(s4);

            Step step1 = new Step
            {
                Id = 1,
                StepName = "Купить",
                NumberOfStep = 1,
                PathToImage = "...",
                Description = "Делаем то",
                InstructionId = 1,
            };
            Step step2 = new Step
            {
                Id = 2,
                StepName = "Приготовить",
                NumberOfStep = 2,
                PathToImage = "...",
                Description = "Делаем сё",
                InstructionId = 1,

            };
            Step step3 = new Step
            {
                Id = 3,
                StepName = "Поджечь",
                NumberOfStep = 3,
                PathToImage = "...",
                Description = "Делаем то-то",
                InstructionId = 1,
            };
            Step step12 = new Step
            {
                Id = 12,
                StepName = "Пукнуть",
                NumberOfStep = 4,
                PathToImage = "...",
                Description = "Делаем ПФФФФФ",
                InstructionId = 1,
            };
            Step step13 = new Step
            {
                Id = 13,
                StepName = "Взлететь",
                NumberOfStep = 5,
                PathToImage = "...",
                Description = "Делаем крыльями взмахи",
                InstructionId = 1,
            };
            Step step4 = new Step
            {
                Id = 1,
                StepName = "Нати",
                NumberOfStep = 1,
                PathToImage = "...",
                Description = "Делаем бла-бла",
                InstructionId = 2,
            };
            Step step5 = new Step
            {
                Id = 2,
                StepName = "Зарегестрироваться",
                NumberOfStep = 1,
                PathToImage = "...",
                Description = "Делаем сё",
                InstructionId = 3,

            };
            Step step6 = new Step
            {
                Id = 3,
                StepName = "Сьесть",
                NumberOfStep = 1,
                PathToImage = "...",
                Description = "Делаем то-то",
                InstructionId = 4,
            };
            
            context.Steps.Add(step1);
            context.Steps.Add(step2);
            context.Steps.Add(step3);
            context.Steps.Add(step4);
            context.Steps.Add(step5);
            context.Steps.Add(step6);
            context.Steps.Add(step12);
            context.Steps.Add(step13);

            Tag c1 = new Tag
            {
                Id = 1,
                TagName = "Взрыв",
                Instructions = new List<Instruction>() { s1, s2 }
            };
            Tag c2 = new Tag
            {
                Id = 2,
                TagName = "Еда",
                Instructions = new List<Instruction>() { s4 }
            };
            Tag c3 = new Tag
            {
                Id = 3,
                TagName = "Веб",
                Instructions = new List<Instruction>() { s3 }
            };

            context.Tags.Add(c1);
            context.Tags.Add(c2);
            context.Tags.Add(c3);

            Comment comment1 = new Comment
            {
                Id = 1,
                UserId = "ba47eb41-7d0a-4012-8023-4418ced312c3",
                Author = "SomeUser",
                InstructionId = 1,
                Contetnt = "Збс взорвал банкомат, Спасибо"
            };
            Comment comment2 = new Comment
            {
                Id = 2,
                UserId = "179738bc-b1cd-45cb-a77a-c72bf94a4e42",
                Author = "Miha_aa123",
                InstructionId = 1,
                Contetnt = "Рад стараться!"
            };
            context.Comments.Add(comment1);
            context.Comments.Add(comment2);

            Rating rating1 = new Rating
            {
                Id = 1,
                UserId = "179738bc-b1cd-45cb-a77a-c72bf94a4e42",
                InstructionId = 1,
                RatingScore = true
            };
            Rating rating2 = new Rating
            {
                Id = 2,
                UserId = "d3fc6311-83e8-4f03-92ce-bc1b128a8414",
                InstructionId = 1,
                RatingScore = true
            };
            Rating rating3 = new Rating
            {
                Id = 3,
                UserId = "179738bc-b1cd-45cb-a77a-c72bf94a4e42",
                InstructionId = 2,
                RatingScore = true
            };
            Rating rating4 = new Rating
            {
                Id = 4,
                UserId = "ba47eb41-7d0a-4012-8023-4418ced312c3",
                InstructionId = 2,
                RatingScore = true
            };
            Rating rating5 = new Rating
            {
                Id = 5,
                UserId = "d3fc6311-83e8-4f03-92ce-bc1b128a8414",
                InstructionId = 2,
                RatingScore = false
            };
            context.Ratings.Add(rating1);
            context.Ratings.Add(rating2);
            context.Ratings.Add(rating3);
            context.Ratings.Add(rating4);
            context.Ratings.Add(rating5);

            Category category1 = new Category
            {
                Id = 1,
                СategoryName = "Химия"
            };
            Category category2 = new Category
            {
                Id = 2,
                СategoryName = "Преступность"
            };
            Category category3 = new Category
            {
                Id = 3,
                СategoryName = "Изобретение"
            };
            Category category4 = new Category
            {
                Id = 4,
                СategoryName = "Лайфхак"
            };

            context.Categories.Add(category1);
            context.Categories.Add(category2);
            context.Categories.Add(category3);
            context.Categories.Add(category4);

            base.Seed(context);
        }
    }
}