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
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, string searchBy)
        {
            var service = CreateMeetingService();
            var rawData = (from m in service.GetMeetings()
                           select m).ToList();
            var meetings = from m in rawData
                           select m;

            //Sorting ViewBags
            ViewBag.DateSortStartParam = sortOrder == "DateStart" ? "DateDescStart" : "DateStart";
            ViewBag.DateSortEndParam = sortOrder == "DateEnd" ? "DateDescEnd" : "DateEnd";
            ViewBag.NameSortParam = sortOrder == "NameAscend" ? "NameDesc" : "NameAscend";

            //Cuurent Filter
            if (searchString != null)
                currentFilter = null;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            // Search Title or Type of Meeting
            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchBy == "TypeOfMeeting")
                    meetings = meetings
                            .Where(s => s.TypeOfMeeting.ToString().ToLower().Contains(searchString.ToLower()) || searchString == null).ToList();
               
                //else if (searchBy == "SearchDate")
                //    students = students
                //             .Where(s => s.EndTime >= DateTime.ParseExact(searchString, "MM/dd/yyyy", CultureInfo.InvariantCulture) || s.StartTime <= DateTime.ParseExact(searchString, "MM/dd/yyyy", CultureInfo.InvariantCulture)).ToList();

                //Title of Meeting
                else
                    meetings = meetings
                             .Where(s => s.NameOfMeeting.ToLower().Contains(searchString.ToLower()));
            }

            // Num of Meetings Today
            ViewBag.TotalNumToday = meetings.Where(s => s.StartTime <= DateTime.Today && s.EndTime >= DateTime.Today).ToList().Count();

            ViewBag.SearchString = searchString;
            switch (sortOrder)
            {
                case "DateDescStart":
                    meetings = meetings.OrderByDescending(s => s.StartTime);
                    break;

                case "DateEnd":
                    meetings = meetings.OrderBy(s => s.EndTime);
                    break;

                case "DateDescEnd":
                    meetings = meetings.OrderByDescending(s => s.EndTime);
                    break;

                case "NameDesc":
                    meetings = meetings.OrderByDescending(s => s.NameOfMeeting);
                    break;

                case "NameAscend":
                    meetings = meetings.OrderBy(s => s.NameOfMeeting);
                    break;

                default:
                    meetings = meetings.OrderBy(s => s.StartTime);
                    break;
            }

            return View(meetings.ToList());
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

            if (model.StartTime > model.EndTime)
            {
                ModelState.AddModelError("", "Start Date CANNOT be after End Date!");
                return View(model);
            }

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

        public ActionResult Edit(int id)
        {
            var service = CreateMeetingService();
            var detail = service.GetMeetingById(id);
            var model =
                new MeetingEdit
                {
                    Id = detail.Id,
                    NameOfMeeting = detail.NameOfMeeting,
                    Description = detail.Description,
                    Location = detail.Location,
                    StartTime = detail.StartTime,
                    EndTime = detail.EndTime,
                   TypeOfMeeting = detail.TypeOfMeeting
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MeetingEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch.");
                return View(model);
            }

            var service = CreateMeetingService();

            if (service.UpdateMeeting(model))
            {
                TempData["Save Result"] = "Your Meeting was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Meeting could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service
                = CreateMeetingService();
            var model = service.GetMeetingById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateMeetingService();

            service.DeleteMeeting(id);

            TempData["SaveResult"] = "Your meeting was deleted";

            return RedirectToAction("Index");
        }

        // Help Methods
        private MeetingService CreateMeetingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MeetingService(userId);
            return service;
        }
    }
}