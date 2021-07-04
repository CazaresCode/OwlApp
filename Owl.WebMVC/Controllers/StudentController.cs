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
        public ActionResult Index(string sortOrder, string searchString)
        {
            var service = CreateStudentService();

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewBag.DateSortStartParam = sortOrder == "DateStart" ? "dateDescStart" : "DateStart";
            ViewBag.DateSortEndParam = sortOrder == "DateEnd" ? "dateDescEnd" : "DateEnd";

            var students = from s in service.GetStudents()
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;

                case "Date":
                    students = students.OrderBy(s => s.StartTime);
                    break;

                case "dateDescStart":
                    students = students.OrderByDescending(s => s.StartTime);
                    break;

                case "DateEnd":
                    students = students.OrderBy(s => s.EndTime);
                    break;

                case "dateDescEnd":
                    students = students.OrderByDescending(s => s.EndTime);
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