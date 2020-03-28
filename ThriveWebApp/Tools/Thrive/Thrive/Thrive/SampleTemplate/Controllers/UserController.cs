using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleTemplate.Models;

namespace SampleTemplate.Controllers
{
    public class UserController : Controller
    {
        DAO dao = new DAO();
        // GET: User
        public ActionResult Index()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.InsertUser(user);
                if (count == 1)
                    ViewBag.Status = "User record is created successfully";
                else ViewBag.Status = "Error! "+dao.message;
                return View("Status");
            }
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {

                if (user.UserRole == Role.Staff && user.Email=="staff@thrive.com" && user.Password=="12345")
                {
                    Session["name"] = "Thrive";
                    Session["email"] = "staff@trive.com";
                    
                    return RedirectToAction("Index", "Staff");
                }
                else if (user.UserRole == Role.Customer)
                {
                    user.FirstName = dao.CheckLogin(user);
                    if (user.FirstName != null)
                    {
                        
                        Session["name"] = user.FirstName;
                        Session["email"] = user.Email;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Status = "Error " + dao.message;

                        return View("Status");
                    }
                   
                }
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}