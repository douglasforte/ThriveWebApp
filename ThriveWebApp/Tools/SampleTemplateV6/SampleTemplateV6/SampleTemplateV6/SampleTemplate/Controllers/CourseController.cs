using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleTemplate.Models;

namespace SampleTemplate.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        DAO dao = new DAO();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCourse(Course course)
        {
            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.InsertCourse(course);
                if (count > 0)
                {
                    Session["message"] = "Course created successfully";
                }
                else
                {
                    Session["message"] = "Error! " + dao.message;

                }
                return RedirectToAction("Index", "Staff");
            }
            return View("Index",course);
        }

        public ActionResult ShowAll()
        {
            List<Course> list = dao.ShowAllCourses();

            return View(list);
        }
    }
}