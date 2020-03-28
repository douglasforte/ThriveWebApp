using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleTemplate.Models;

namespace SampleTemplate.Controllers
{
    public class ModuleController : Controller
    {
        DAO dao = new DAO();
        static List<Section> sectionList = new List<Section>();
        
        // GET: Module
        public ActionResult Index()
        {
            
            ViewData["titles"] = GetCourseTitles();
            return View();
        }

        [HttpPost]
        public ActionResult AddModule(Module module)
        {
            ViewData["titles"] = GetCourseTitles();
            if (ModelState.IsValid)
            {
                TempData["module"] = module;
                return RedirectToAction("AddSection");
            }
             return View("Index",module);
        }

        
        public ActionResult AddSection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSection(Section section)
        {
            if (ModelState.IsValid)
            {
                sectionList.Add(section);
                ViewData["message"] = "Section added succesfully";
                return View();
            }
            return View(section);
        }

       
        public ActionResult CompleteModule()
        {
            int count = 0;
            Module module = (Module)TempData["module"];
            module.Syllabus = sectionList;
            count = dao.InsertModule(module);
            if (count > 0)
            {
                Session["mod_message"] = "Module created succesfully";

            }
            else
            {
                Session["mod_message"] = "Error! " + dao.message;
             }

            return RedirectToAction("Index");
        }
        private List<string> GetCourseTitles()
        {
            List<string> titlesList = new List<string>();
            List<Course> courseList = dao.ShowAllCourses();

            foreach (Course course in courseList)
            {
                titlesList.Add(course.Title);
            }
            return titlesList;
        }

        public ActionResult FindModule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindModule(Module module)
        {
            ModelState.Remove("ModuleTitle");
            ModelState.Remove("Hours");
            Module module1=null;
            if (ModelState.IsValid)
            {
                module1 = dao.FindModule(module.ModuleCode);
                if (module1 == null)
                {
                    ViewData["message"] = "No module! check again";
                }
                else return View("ShowModule",module1);

            }
            return View(module);
        }
    }
}