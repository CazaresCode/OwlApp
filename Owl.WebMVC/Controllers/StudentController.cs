using Microsoft.AspNet.Identity;
using Owl.Models.StudentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Owl.WebMVC.Controllers
{
    [Authorize] 
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            var model = new StudentListItem[0];
            return View(model);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateStudentService();

            if (service.CreateStudent(model))
            {
                TempData["SaveResult"] = "Your Student was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Student could not be created.");

            return View(model);
        }

        // Helper Method
        private StudentService CreateStudentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new StudentService(userId);
            return service;
        }
    }
}