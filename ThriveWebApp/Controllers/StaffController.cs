using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThriveWebApp.Models;

namespace ThriveWebApp.Controllers
{
    public class StaffController : Controller
    {
        DAO dao = new DAO();
        // GET: Staff

        public ActionResult Index()
        {
            return View();
        }
        
        

    }
}