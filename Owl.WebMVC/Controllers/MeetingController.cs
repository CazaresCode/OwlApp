using Microsoft.AspNet.Identity;
using Owl.Models.MeetingModels;
using Owl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Owl.WebMVC.Controllers
{
    [Authorize]
    public class MeetingController : Controller
    {
        // GET: Meeting
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MeetingService(userId);
            var model = service.GetMeetings();

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
        public ActionResult Create(MeetingCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateMeetingService();

            if (service.CreateMeeting(model))
            {
                TempData["SaveResult"] = "Your Meeting was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Meeting could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateMeetingService();
            var model = svc.GetMeetingById(id);
            return View(model);
        }

        private MeetingService CreateMeetingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MeetingService(userId);
            return service;
        }
    }
}