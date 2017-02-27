﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coursework.Filters;
using Coursework.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace Coursework.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        public PostEntities db = new PostEntities();
        public ActionResult Index(string filter)
        {
            if (filter == null)
            {
                ViewBag.Filter = "new";
            }
            else
            {
                ViewBag.Filter = filter;
            }
            //var context = new ApplicationDbContext();
            //var allUsers = context.Users.ToList();
            //var allRoles = context.Roles.ToList();
            return View();
        }

        public ActionResult ShowInstructions(string user_id = "")
        {
            List<Instruction> list = new List<Instruction>();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user_id);
            if (user != null)
            {
                ViewBag.UserName = user.UserName;
                ViewBag.UserId = user_id;
                ViewBag.Filter = "MainInstruction";
                list = db.Instructions.Where(x => x.UserId == user_id).ToList();
                ViewBag.Count = list.Count;
                return View();
            }
            return HttpNotFound();
        }
        public ActionResult InstructionList(string user_id = "", string filter ="")
        {
            if (filter == "new")
            {
                return PartialView(db.Instructions.OrderByDescending(x => x.DateOfCreation));
            }
            if (filter == "best")
            {
                return PartialView(db.Instructions.OrderByDescending(x => x.NumberOfLikes));
            }
            ViewBag.UserId = User.Identity.GetUserId();
            List<Instruction> list = new List<Instruction>();
            list = db.Instructions.Where(x => x.UserId == user_id)
                .OrderByDescending(x => x.DateOfCreation).ToList();
            return PartialView(list);
        }
        
        public ActionResult RedactInstruction(int id)
        {
            Instruction instruction = db.Instructions.FirstOrDefault(t => t.Id == id);
            if (instruction == null || instruction.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            SelectList categories = new SelectList(db.Categories, "Id", "СategoryName");
            ViewBag.Categories = categories;
            ViewBag.Tags = db.Tags.ToList();
            return View(instruction);
        }

        [HttpPost]
        public void DeleteStep(int NumberOfStep, int InstructionId)
        {
            Step step = db.Steps.Where(s=> s.InstructionId == InstructionId).
                FirstOrDefault(s => s.NumberOfStep == NumberOfStep);
            if (step != null)
            {
                db.Steps.Remove(step);
                db.SaveChanges();
            }
        }
        
        [HttpPost]
        public void DeleteInstruction(int id)
        {
            Instruction instructions = db.Instructions
                .Include(s=>s.Steps)
                .Include(s=>s.Comments)
                .Include(s=> s.Ratings)
                .FirstOrDefault(c => c.Id == id);

            if (instructions != null)
            {
                db.Instructions.Remove(instructions);
                db.SaveChanges();
            }
        }

        [HttpPost]
        public ActionResult SaveSteps(IEnumerable<Step> steps)
        {
            foreach (Step step in steps)
            {
                Step newStep = db.Steps
                    .Where(s => s.InstructionId == step.InstructionId)
                    .FirstOrDefault(s => s.StepName == step.StepName);
                if (newStep == null)
                {
                    db.Steps.Add(step);
                }
                else
                {
                    newStep.NumberOfStep = step.NumberOfStep;
                    db.Entry(newStep).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            return Json(new { Message = "Successfully" });
            //string returnUrl = Request.UrlReferrer.AbsolutePath;
            //return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult SaveInstructionDescription(Instruction instruction, int[] selectedTags)
        {
            if (ModelState.IsValid) { 
            Instruction newInstruction = db.Instructions.Find(instruction.Id);
            newInstruction.InstructionName = instruction.InstructionName;
            newInstruction.LinkToVideo = instruction.LinkToVideo;
            newInstruction.CategoryId = instruction.CategoryId;
            
            newInstruction.Tags.Clear();
            if (selectedTags != null)
            {
                foreach (var c in db.Tags.Where(co => selectedTags.Contains(co.Id)))
                {
                    newInstruction.Tags.Add(c);
                }
            }
            db.Entry(newInstruction).State = EntityState.Modified;
            db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            return Redirect(returnUrl);
        }
        
        public ActionResult CreateInstruction()
        {
            SelectList categories = new SelectList(db.Categories, "Id", "СategoryName");
            ViewBag.Categories = categories;
            ViewBag.Tags = db.Tags.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CreateInstruction(Instruction instruction, int[] selectedTags)
        {
                instruction.UserId = User.Identity.GetUserId();
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
                instruction.Author = user.UserName;
                instruction.DateOfCreation = DateTime.Now;
                instruction.NumberOfLikes = 0;

                if (selectedTags != null)
                {
                    foreach (var c in db.Tags.Where(co => selectedTags.Contains(co.Id)))
                    {
                        instruction.Tags.Add(c);
                    }
                }
                db.Instructions.Add(instruction);
                db.SaveChanges();
                return RedirectToAction("ShowInstructions", new { user_id = User.Identity.GetUserId() });
        }

        public ActionResult RedactStep(int NumberOfStep, int InstructionId)
        {
            Step step = db.Steps.Where(s => s.InstructionId == InstructionId).
                FirstOrDefault(s => s.NumberOfStep == NumberOfStep);
            Instruction instruction = db.Instructions.FirstOrDefault(i => i.Id == step.InstructionId);
            
            if (instruction == null  || step == null || instruction.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            return View(step);
        }

        [HttpPost]
        public ActionResult RedactStep(Step step)
        {
            Step newStep = db.Steps.Find(step.Id);
            newStep.StepName = step.StepName;
            newStep.PathToImage = step.PathToImage;
            newStep.Description = step.Description;

            db.Entry(newStep).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("RedactInstruction", new { id = newStep.InstructionId });
        }

        [HttpPost]
        public ActionResult StepsPartial(int id)
        {
            List<Step> list = new List<Step>();
            using (var db = new PostEntities())
            {
                list = db.Steps.Where(x => x.InstructionId == id)
                    .OrderBy(x => x.NumberOfStep)
                    .ToList();
            }
            ViewBag.InstructionId = id;
            return PartialView(list);
        }

        public ActionResult SaveUploadedFile()
        {
            return Json(new { Message = "Error in saving file" });
        }

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // Список культур
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            // Сохраняем выбранную культуру в куки
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
        
    }
}