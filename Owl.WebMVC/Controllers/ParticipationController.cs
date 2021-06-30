using Microsoft.AspNet.Identity;
using Owl.Models.ParticipationModels;
using Owl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Owl.WebMVC.Controllers
{
    public class ParticipationController : Controller
    {
        // GET: Participation
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new StudentService(userId);
            var model = service.GetStudents();

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
        public ActionResult Create(ParticipationCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateParticipationService();

            if (service.CreateParticipation(model))
            {
                TempData["SaveResult"] = "Your Participation was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Participation could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateParticipationService();
            var model = svc.GetParticipationById(id);
            return View(model);
        }

        private ParticipationService CreateParticipationService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ParticipationService(userId);
            return service;
        }
    }
}