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
            List<Person> people = new PersonService().GetPeople().ToList();
            ViewBag.PersonId = people.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.FullName,
            });

            var userId = Guid.Parse(User.Identity.GetUserId());

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

        public ActionResult Edit(int id)
        {
            var service = CreateParticipationService();
            var detail = service.GetParticipationById(id);

            var userId = Guid.Parse(User.Identity.GetUserId());

            List<MeetingListItem> meetings = new MeetingService(userId).GetMeetings().ToList();
            ViewBag.MeetingId = meetings.Select(m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = m.NameOfMeeting,
            });

            List<Person> people = new PersonService().GetPeople().ToList();
            ViewBag.PersonId = people.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.FullName,
            });

            var model =
                new ParticipationEdit
                {
                    Id = detail.Id,
                    PersonId = detail.PersonId,
                    MeetingId = detail.MeetingId
                };

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include ="Id, MeetingId, PersonId")] ParticipationEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch.");
                return View(model);
            }

            var service = CreateParticipationService();

            if (service.UpdateParticipation(model))
            {
                TempData["Save Result"] = "Your Participation was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Participation could not be updated.");
            return View(model);
        }


        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service
                = CreateParticipationService();
            var model = service.GetParticipationById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateParticipationService();

            service.DeleteParticipation(id);

            TempData["SaveResult"] = "Your participation was deleted";

            return RedirectToAction("Index");
        }



        // Helper Methods

        // Future helper method for the viewbag stuff
        //private void PopulateMeetingDropDownList(object selectedMeeting = null)
        //{
        //    var service = CreateParticipationService();
        //    var meetingsQuery = from m in service.GetParticipations()
        //                           orderby m.Meeting.NameOfMeeting
        //                           select m;
        //    ViewBag.MeetingId = new SelectList(meetingsQuery, "MeetingId", "NameOfMeeting", selectedMeeting);
        //}




        private ParticipationService CreateParticipationService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ParticipationService(userId);
            return service;
        }
    }
}