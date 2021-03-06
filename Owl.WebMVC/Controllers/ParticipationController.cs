using Microsoft.AspNet.Identity;
using Owl.Data.EntityModels;
using Owl.Models.MeetingModels;
using Owl.Models.ParticipationModels;
using Owl.Models.PersonModels;
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
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, string searchBy)
        {
            var service = CreateParticipationService();
            var model = service.GetParticipations();

            var rawData = (from p in service.GetParticipations()
                           select p).ToList();
            var participations = from p in rawData
                                 select p;

            //Sorting ViewBags
            ViewBag.DateSortStartParam = sortOrder == "DateStart" ? "DateDescStart" : "DateStart";
            ViewBag.DateSortEndParam = sortOrder == "DateEnd" ? "DateDescEnd" : "DateEnd";
            ViewBag.FirstNameSortParam = sortOrder == "FirstNameAscend" ? "FirstNameDesc" : "FirstNameAscend";
            ViewBag.LastNameSortParam = sortOrder == "LastNameAscend" ? "LastNameDesc" : "LastNameAscend";
            ViewBag.MeetingTypeSortParam = sortOrder == "MeetingTypeAscend" ? "MeetingTypeDesc" : "MeetingTypeAscend";
            ViewBag.MeetingNameSortParam = sortOrder == "MeetingNameAscend" ? "MeetingNameDesc" : "MeetingNameAscend";

            //Cuurent Filter
            if (searchString != null)
                currentFilter = null;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            // Search Title or Type of Meeting
            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchBy == "Meeting")
                    participations = participations
                                .Where(s => s.Meeting.TypeOfMeeting.ToString().ToLower().Contains(searchString.ToLower()) || s.Meeting.NameOfMeeting.ToLower().Contains(searchString.ToLower()) || searchString == null).ToList();

                //else if (searchBy == "SearchDate")
                //    students = students
                //             .Where(s => s.EndTime >= DateTime.ParseExact(searchString, "MM/dd/yyyy", CultureInfo.InvariantCulture) || s.StartTime <= DateTime.ParseExact(searchString, "MM/dd/yyyy", CultureInfo.InvariantCulture)).ToList();

                //Title of Meeting
                else
                    participations = participations
                             .Where(s => s.Person.LastName.ToLower().Contains(searchString.ToLower()) || s.Person.FirstName.ToLower().Contains(searchString.ToLower()));
            }

            ViewBag.SearchString = searchString;

            switch (sortOrder)
            {

                case "DateEnd":
                    participations = participations.OrderBy(s => s.Meeting.EndTime);
                    break;
                case "DateDescEnd":
                    participations = participations.OrderByDescending(s => s.Meeting.EndTime);
                    break;

                case "MeetingNameAscend":
                    participations = participations.OrderBy(s => s.Meeting.NameOfMeeting);
                    break;
                case "MeetingNameDesc":
                    participations = participations.OrderByDescending(s => s.Meeting.NameOfMeeting);
                    break;

                case "MeetingTypeAscend":
                    participations = participations.OrderBy(s => s.Meeting.TypeOfMeeting);
                    break;
                case "MeetingTypeDesc":
                    participations = participations.OrderByDescending(s => s.Meeting.TypeOfMeeting);
                    break;

                case "FirstNameAscend":
                    participations = participations.OrderBy(s => s.Person.FirstName);
                    break;
                case "FirstNameDesc":
                    participations = participations.OrderByDescending(s => s.Person.FirstName);
                    break;

                case "LastNameAscend":
                    participations = participations.OrderBy(s => s.Person.LastName);
                    break;
                case "LastNameDesc":
                    participations = participations.OrderByDescending(s => s.Person.LastName);
                    break;

                case "DateDescStart":
                    participations = participations.OrderByDescending(s => s.Meeting.StartTime);
                    break;
                //StartTime Default
                default:
                    participations = participations.OrderBy(s => s.Meeting.StartTime);
                    break;
            }

            return View(participations);
        }

        // GET: Create
        public ActionResult Create()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());

            var personService = new PersonService(userId);
            var personList = personService.GetPeopleList();

            ViewBag.PersonId = personList.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.FullName,
            });

            var meetingService = new MeetingService(userId);
            var meetingList = meetingService.GetMeetings();

            ViewBag.MeetingId = meetingList.Select(m => new SelectListItem()
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


            var userId = Guid.Parse(User.Identity.GetUserId());

            var meetingId = model.MeetingId;
            var personId = model.PersonId;

            //PersonService
            var personService = new PersonService(userId);
            var personDetails = personService.GetPersonById(personId);
            var pStartTime = personDetails.StartTime;
            var pEndTime = personDetails.EndTime;

            //MeetingService
            var meetingService = new MeetingService(userId);
            var meetingDetails = meetingService.GetMeetingById(meetingId);
            var mStartTime = meetingDetails.StartTime;
            var mEndTime = meetingDetails.EndTime;

            //How to retrieve the data but give the error? Something with the dropdown in the create...
            if (mStartTime <= pStartTime && mEndTime <= pStartTime && mStartTime >= pEndTime && mEndTime >= pEndTime)
            {
                ModelState.AddModelError("", "Meeting is not within the Person's Date range.");
                return View(model);
            }

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

            var peopleService = new PersonService(userId);
            var peopleList = peopleService.GetPeopleList();

            ViewBag.PersonId = peopleList.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.FullName,
            });

            List<MeetingListItem> meetings = new MeetingService(userId).GetMeetings().ToList();
            ViewBag.MeetingId = meetings.Select(m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = m.NameOfMeeting,
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
        public ActionResult Edit(int id, [Bind(Include = "Id, MeetingId, PersonId")] ParticipationEdit model)
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

        private ParticipationService CreateParticipationService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ParticipationService(userId);
            return service;
        }
    }
}