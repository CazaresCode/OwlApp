using Microsoft.AspNet.Identity;
using Owl.Data.EntityModels;
using Owl.Models.MeetingModels;
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
            var service = new ParticipationService(userId);
            var model = service.GetParticipations();

            return View(model);
        }

        // GET: Create
        public ActionResult Create()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());

            List<Person> people = new PersonService().GetPeople().ToList();
            ViewBag.PersonId = people.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.LastName,
            });


            List<MeetingListItem> meetings = new MeetingService(userId).GetMeetings().ToList();
            ViewBag.MeetingId = meetings.Select(m=> new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = m.NameOfMeeting,
            });

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