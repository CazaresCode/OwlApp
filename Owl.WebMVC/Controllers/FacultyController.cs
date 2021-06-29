using Microsoft.AspNet.Identity;
using Owl.Models.FacultyModels;
using Owl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Owl.WebMVC.Controllers
{
    public class FacultyController : Controller
    {
        // GET: Faculty
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FacultyService(userId);
            var model = service.GetFaculty();

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
        public ActionResult Create(FacultyCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateFacultyService();

            if (service.CreateFaculty(model))
            {
                TempData["SaveResult"] = "Your Faculty was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Faculty could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateFacultyService();
            var model = svc.GetFacultyById(id);
            return View(model);
        }

        private FacultyService CreateFacultyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FacultyService(userId);
            return service;
        }
    }
}