using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleTemplate.Models;

namespace SampleTemplate.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        DAO dao = new DAO();
        public ActionResult Index()
        {

            List<Book> booklist = dao.ShowAllBooks();
            //new List<Book>()
            //{
            //    new Book("13232732","C++ Programming","Paul Murphy",new DateTime(2016,09,08),45.00m,"CPlus.png"),
            //    new Book("47584745","Java Programming","Orla Kelly",new DateTime(2017,01,02),34.00m,"Java.png"),
            //    new Book("243284783","Intro to Programming","John Murphy",new DateTime(2016,07,03),50.00m,"Programming.png"),
            //    new Book("65765768","Python Programming","Pat Smith",new DateTime(2015,06,12),45.00m,"Python.png"),
            //};
            List<int> quantityList = new List<int>() { 1, 2, 3, 4, 5 };
            ViewBag.Quantity = quantityList;
            return View(booklist);
        }
    }
}