using System;
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
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var allUsers = context.Users.ToList();
            var allRoles = context.Roles.ToList();
            return View(allUsers);
        }

        public ActionResult ShowInstructions(string user_id = "")
        {
            List<Instruction> list = new List<Instruction>();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user_id);
            if (user != null)
            {
                ViewBag.UserName = user.UserName;
                ViewBag.UserId = user_id;
                list = db.Instructions.Where(x => x.UserId == user_id).ToList();
                ViewBag.Count = list.Count;
                return View();
            }
            return HttpNotFound();
        }
        public ActionResult InstructionList(string user_id = "")
        {
            ViewBag.UserId = user_id;
            List<Instruction> list = new List<Instruction>();
            list = db.Instructions.Where(x => x.UserId == user_id).ToList();
            return PartialView(list);

        }
        

        public ActionResult RedactInstruction(int id)
        {
            Instruction instruction = db.Instructions.FirstOrDefault(t => t.Id == id);
            if (instruction == null || instruction.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            return View(instruction);
        }

        [HttpPost]
        public void DeleteStep(int id)
        {
            Step step = db.Steps.FirstOrDefault(s => s.Id == id);
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



            string returnUrl = Request.UrlReferrer.AbsolutePath;
            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult StepsPartial(int id)
        {
            List<Step> list = new List<Step>();
            using (var db = new PostEntities())
            {
                list = db.Steps.Where(x => x.InstructionId == id).ToList();
            }
            return PartialView(list);
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