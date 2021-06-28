using Owl.Models.StudentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Owl.WebMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            var model = new StudentListItem[0];
            return View(model);
        }
    }
}