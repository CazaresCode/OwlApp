using Microsoft.AspNet.Identity;
using Owl.Models.StudentModels;
using Owl.Services;
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
        public ActionResult Index(string sortOrder, string searchString, string selectedFirstName, string selectedLastName)
        {
            var service = CreateStudentService();

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewBag.FirstNameSortParam = sortOrder == "FirstNameAscend" ? "FirstNameDesc" : "FirstNameAscend";
            ViewBag.DateSortStartParam = sortOrder == "DateStart" ? "DateDescStart" : "DateStart";
            ViewBag.DateSortEndParam = sortOrder == "DateEnd" ? "DateDescEnd" : "DateEnd";
            ViewBag.HasPaidParam = sortOrder == "HasPaidTuition" ? "HasNotPaidTuition" : "HasPaidTuition";

            var rawData = (from s in service.GetStudents()
                           select s).ToList();

            var students = from s in rawData
                           select s;


            // Search First or Last name
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s =>
                                        s.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                        s.LastName.ToLower().Contains(searchString.ToLower()));
            }
            //IS THIS NEEDED?
            ViewBag.SearchString = searchString;

            // Filter Last Name
            if (!String.IsNullOrEmpty(selectedLastName))
            {
                students = students.Where(s => s.LastName.Trim().Equals(selectedLastName.Trim()));
            }
            ViewBag.SelectedLastName = selectedLastName;

            // Filter First Name
            if (!String.IsNullOrEmpty(selectedFirstName))
            {
                students = students.Where(s => s.FirstName.Trim().Equals(selectedFirstName.Trim()));
            }
            ViewBag.SelectedFirstName = selectedFirstName;

            // Last Names
            var uniqueLastNames = from s in students
                                  group s by s.LastName into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { LastName = newGroup.Key };

            ViewBag.UniqueLastNames = uniqueLastNames.Select(n => new SelectListItem { Value = n.LastName, Text = n.LastName }).ToList();

            // First Names
            var uniqueFirstNames = from s in students
                                   group s by s.FirstName into newGroup
                                   where newGroup.Key != null
                                   orderby newGroup.Key
                                   select new { FirstName = newGroup.Key };

            ViewBag.UniqueFirstNames = uniqueFirstNames.Select(n => new SelectListItem { Value = n.FirstName, Text = n.FirstName }).ToList();

            switch (sortOrder)
            {
                case "NameDesc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;

                case "FirstNameDesc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;

                case "FirstNameAscend":
                    students = students.OrderBy(s => s.FirstName);
                    break;

                case "DateStart":
                    students = students.OrderBy(s => s.StartTime);
                    break;

                case "DateDescStart":
                    students = students.OrderByDescending(s => s.StartTime);
                    break;

                case "DateEnd":
                    students = students.OrderBy(s => s.EndTime);
                    break;

                case "DateDescEnd":
                    students = students.OrderByDescending(s => s.EndTime);
                    break;

                case "HasPaidTuition":
                    students = students.OrderBy(s => s.HasPaidTuition);
                    break;

                case "HasNotPaidTuition":
                    students = students.OrderByDescending(s => s.HasPaidTuition);
                    break;

                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            return View(students.ToList());
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

        public ActionResult Details(int id)
        {
            var svc = CreateStudentService();
            var model = svc.GetStudentById(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateStudentService();
            var detail = service.GetStudentById(id);
            var model =
                new StudentEdit
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
                    TypeOfProgram = detail.TypeOfProgram,
                    HasPaidTuition = detail.HasPaidTuition
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StudentEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch.");
                return View(model);
            }

            var service = CreateStudentService();

            if (service.UpdateStudent(model))
            {
                TempData["Save Result"] = "Your Student was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Student could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service
                = CreateStudentService();
            var model = service.GetStudentById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateStudentService();

            service.DeleteStudent(id);

            TempData["SaveResult"] = "Your student was deleted";

            return RedirectToAction("Index");
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