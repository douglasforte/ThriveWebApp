using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThriveWebApp.Models;
using System.Data;

namespace ThriveWebApp.Controllers
{
    public class ContactController : Controller
    {
        static DataSet ds;
        static DataTable dt;

        // GET: Contact
        public ActionResult Index()
        {
            if (System.IO.File.Exists(Server.MapPath("~/App_Data/feedback.xml")))
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/App_Data/feedback.xml"));
                dt = ds.Tables[0];
            }
            else
            {

                ds = new DataSet("user_feedback");
                dt = new DataTable("user_comments");
                dt.Columns.Add("name");
                dt.Columns.Add("email");
                dt.Columns.Add("comments");
                ds.Tables.Add(dt);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Contact model)
        {
            if (ModelState.IsValid)
            {
                DataRow row = dt.NewRow();
                if (model.Name == "" || model.Name == null)
                {
                    row["name"] = "name not entered";
                }
                else row["name"] = model.Name;
                row["email"] = model.Email;
                row["comments"] = model.Comments;
                dt.Rows.Add(row);
                dt.AcceptChanges();
                ds.WriteXml(Server.MapPath("~/App_Data/feedback.xml"));
                ViewData["message"] = "Record inserted successfully";
                return RedirectToAction("Index","Home");
            }

            return View(model);
        }

        public ActionResult ShowFeedback()
        {
            List<Contact> list = new List<Contact>();
            if (System.IO.File.Exists(Server.MapPath("~/App_Data/feedback.xml")))
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(Server.MapPath("~/App_Data/feedback.xml"));
                DataTable table = dataSet.Tables[0];
                foreach(DataRow row in table.Rows)
                {
                    Contact model = new Contact();
                    model.Name = row[0].ToString();
                    model.Email = row[1].ToString();
                    model.Comments = row[2].ToString();
                    list.Add(model);
                }
                ViewData["message"] = "";
            }

            else
            {
                ViewData["message"] = "User feedback is not recorded";
            }

            return View(list);
        }
    }
}