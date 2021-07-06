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
        public ActionResult Index(string sortOrder, string searchString, string selectedFirstName, string selectedLastName, string currentFilter)
        {
            var service = CreateFacultyService();
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewBag.FirstNameSortParam = sortOrder == "FirstNameAscend" ? "FirstNameDesc" : "FirstNameAscend";
            ViewBag.DateSortStartParam = sortOrder == "DateStart" ? "DateDescStart" : "DateStart";
            ViewBag.DateSortEndParam = sortOrder == "DateEnd" ? "DateDescEnd" : "DateEnd";
            ViewBag.IsStaffParam = sortOrder == "IsStaffSort" ? "IsNotStaffSort" : "IsStaffSort";
            ViewBag.HasFoodAllergyParam = sortOrder == "HasFoodAllergySort" ? "HasNoFoodAllergySort" : "HasFoodAllergySort";


            var rawData = (from s in service.GetFaculty()
                           select s).ToList();

            var faculties = from s in rawData
                           select s;

            if (searchString != null)
            {
                currentFilter = null;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            // Search First or Last name
            if (!String.IsNullOrEmpty(searchString))
            {
                faculties = faculties.Where(s =>
                                        s.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                        s.LastName.ToLower().Contains(searchString.ToLower()));
            }
            //IS THIS NEEDED?
            ViewBag.SearchString = searchString;

            // Filter Last Name
            if (!String.IsNullOrEmpty(selectedLastName))
            {
                faculties = faculties.Where(s => s.LastName.Trim().Equals(selectedLastName.Trim()));
            }
            ViewBag.SelectedLastName = selectedLastName;

            // Filter First Name
            if (!String.IsNullOrEmpty(selectedFirstName))
            {
                faculties = faculties.Where(s => s.FirstName.Trim().Equals(selectedFirstName.Trim()));
            }
            ViewBag.SelectedFirstName = selectedFirstName;

            switch (sortOrder)
            {
                case "NameDesc":
                    faculties = faculties.OrderByDescending(s => s.LastName);
                    break;

                case "FirstNameDesc":
                    faculties = faculties.OrderByDescending(s => s.FirstName);
                    break;

                case "FirstNameAscend":
                    faculties = faculties.OrderBy(s => s.FirstName);
                    break;

                case "DateStart":
                    faculties = faculties.OrderBy(s => s.StartTime);
                    break;

                case "DateDescStart":
                    faculties = faculties.OrderByDescending(s => s.StartTime);
                    break;

                case "DateEnd":
                    faculties = faculties.OrderBy(s => s.EndTime);
                    break;

                case "DateDescEnd":
                    faculties = faculties.OrderByDescending(s => s.EndTime);
                    break;

                case "IsStaffSort":
                    faculties = faculties.OrderBy(s => s.IsStaff);
                    break;

                case "IsNotStaffSort":
                    faculties = faculties.OrderByDescending(s => s.IsStaff);
                    break;

                case "HasFoodAllergySort":
                    faculties = faculties.OrderBy(s => s.HasFoodAllergy);
                    break;

                case "HasNoFoodAllergySort":
                    faculties = faculties.OrderByDescending(s => s.HasFoodAllergy);
                    break;

                default:
                    faculties = faculties.OrderBy(s => s.LastName);
                    break;
            }

            return View(faculties.ToList());
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

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service
                = CreateFacultyService();
            var model = service.GetFacultyById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateFacultyService();

            service.DeleteFaculty(id);

            TempData["SaveResult"] = "Your faculty was deleted";

            return RedirectToAction("Index");
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