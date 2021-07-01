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
        [Authorize]
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

        public ActionResult Edit(int id)
        {
            var service = CreateFacultyService();
            var detail = service.GetFacultyById(id);
            var model =
                new FacultyEdit
                {
                    Id = detail.Id,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    Email = detail.Email,
                    PhoneNumber = detail.PhoneNumber,
                    TypeOfInstrument = detail.TypeOfInstrument,
                    StartTime = detail.StartTime,
                    EndTime = detail.EndTime,
                    HasFoodAllergy = detail.HasFoodAllergy,
                    FoodAllergy = detail.FoodAllergy,
                    IsPerforming = detail.IsPerforming,
                    IsStaff = detail.IsStaff
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FacultyEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch.");
                return View(model);
            }

            var service = CreateFacultyService();

            if (service.UpdateFaculty(model))
            {
                TempData["Save Result"] = "Your Faculty was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Faculty could not be updated.");
            return View(model);
        }


        // Helper Method
        private FacultyService CreateFacultyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FacultyService(userId);
            return service;
        }
    }
}