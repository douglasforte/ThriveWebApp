using System.Collections.Generic;
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
            if (System.IO.File.Exists(Server.MapPath("~/Content/feedback.xml")))
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/Content/feedback.xml"));
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
        public ActionResult Index(Contact contact)
        {
            if (ModelState.IsValid)
            {
                DataRow row = dt.NewRow();
                if (contact.Name == "" || contact.Name == null)
                {
                    row["name"] = "name not entered";
                }
                else
                {
                    row["name"] = contact.Name;
                }

                row["email"] = contact.Email;
                row["comments"] = contact.Comments;
                dt.Rows.Add(row);
                dt.AcceptChanges();
                ds.WriteXml(Server.MapPath("~/Content/feedback.xml"));
                ViewData["message"] = "Thank you for your feedback, will get back to soon";
                return RedirectToAction("Index", "Home");
            }

            return View(contact);
        }

        public ActionResult ShowFeedback()
        {
            List<Contact> list = new List<Contact>();
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();

            if (System.IO.File.Exists(Server.MapPath("~/Content/feedback.xml")))
            {
                dataSet.ReadXml(Server.MapPath("~/Content/feedback.xml"));
                table = dataSet.Tables[0];

                foreach (DataRow row in table.Rows)
                {
                    Contact contact = new Contact();
                    contact.Name = row[0].ToString();
                    contact.Email = row[1].ToString();
                    contact.Comments = row[2].ToString();
                    list.Add(contact);
                }

                ViewData["message"] = "User feedback recorded";
            }
            else
            {
                ViewData["message"] = "User feedback is not recorded";
            }

            return View(list);
        }
    }
}