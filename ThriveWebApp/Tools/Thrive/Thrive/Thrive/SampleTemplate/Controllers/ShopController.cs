using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleTemplate.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Iphone7()
        {
            return View();
        }
        public ActionResult Iphone8()
        {
            return View();
        }
        public ActionResult IphoneXR()
        {
            return View();
        }
    }
}