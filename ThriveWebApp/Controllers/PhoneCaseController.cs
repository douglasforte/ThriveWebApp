using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThriveWebApp.Models;

namespace ThriveWebApp.Controllers
{
    public class PhoneCaseController : Controller
    {
        DAO dao = new DAO();

        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PhoneCase phoneCase)
        {
            int count = 0;

            count = dao.InsertPhonecase(phoneCase);
            if (count == 1)
            {
                Response.Write("Item reserved is successfully");
                ModelState.Clear();
            }

            else
            {
                ViewBag.Status = "Error!" + dao.message;
            }

            return View(phoneCase);
        }

        [HttpPost]
        public ActionResult ShowOrder()
        {
            List<PhoneCase> phonecaseList = dao.ShowAllPhoneCases();

            return View(phonecaseList);
        }



    }
}