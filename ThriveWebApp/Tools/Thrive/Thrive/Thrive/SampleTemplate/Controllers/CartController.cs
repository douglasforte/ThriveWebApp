using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleTemplate.Models;

namespace SampleTemplate.Controllers
{
    public class CartController : Controller
    {
        DAO dao = new DAO();
        static List<Book> selectedBooks = new List<Book>();
        static List<Course> selectedCourses = new List<Course>();
        static List<ItemModel> selectedItems = new List<ItemModel>();

        decimal totalPrice = 0.0m;
        decimal totalPriceBook = 0.0m;
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(FormCollection form)
        {

            List<Book> list = dao.ShowAllBooks();
            bool found = false, found1 = false;

            for (int i = 0; i < selectedItems.Count && found == false; i++)
            {
                //if already in the selected items
                if (selectedItems[i].ItemId == form["isbn"])
                {
                    selectedItems[i].Quantity = selectedItems[i].Quantity + int.Parse(form["quantity"]);
                    selectedItems[i].TotalPrice = selectedItems[i].TotalPrice * selectedItems[i].Quantity;
                    found = true;
                    found1 = true;

                }

            }

            for (int i = 0; i < list.Count && found1 == false; i++)
            {

                if (list[i].ISBN == form["isbn"])
                {
                    list[i].Quantity = int.Parse(form["quantity"]);
                    selectedBooks.Add(list[i]);
                    found1 = true;

                }
            }


            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public ActionResult AddCourse(FormCollection form)
        {

            List<Course> list = dao.ShowAllCourses();
            bool found = false;

            for (int i = 0; i < list.Count && found == false; i++)
            {

                if (list[i].CourseCode == form["code"])
                {

                    selectedCourses.Add(list[i]);
                    found = true;

                }
            }


            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public ActionResult RemoveItem(FormCollection form)
        {
            bool found = false;
            ItemModel item;

            for (int i = 0; i < selectedItems.Count && found == false; i++)
            {

                if (selectedItems[i].ItemId == form["id"] || selectedItems[i].ItemId == form["code"])
                {
                    item = selectedItems[i];
                    if (item.Quantity > 1)
                    {
                        item.Quantity = item.Quantity - 1;
                        item.TotalPrice = item.TotalPrice - item.Price;
                    }
                    else
                    {
                        selectedItems.Remove(item);
                    }
                    found = true;
                }
            }
            foreach (var item1 in selectedItems)
            {
                totalPrice = totalPrice + item1.TotalPrice;
            }
            ViewBag.TransactionPrice = totalPrice;

            return RedirectToAction("ViewCart");
        }

        public ActionResult ClearAll()
        {
            selectedBooks.Clear();
            selectedItems.Clear();
            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {


            foreach (Book book in selectedBooks)
            {

                ItemModel item = new ItemModel();

                totalPriceBook = totalPriceBook + book.Quantity * book.Price;
                item.ItemId = book.ISBN;
                item.Title = book.Title;
                item.Quantity = book.Quantity;
                item.Price = book.Price;
                item.TotalPrice = totalPriceBook;
                if (!selectedItems.Contains(item))
                    selectedItems.Add(item);

            }
            foreach (Course course in selectedCourses)
            {

                ItemModel item = new ItemModel();

                item.ItemId = course.CourseCode;
                item.Title = course.Title;
                item.Quantity = 1;
                item.Price = course.Price;
                item.TotalPrice = course.Price;
                if (!selectedItems.Contains(item))
                    selectedItems.Add(item);

            }
            foreach (var item in selectedItems)
            {
                totalPrice = totalPrice + item.TotalPrice;
            }
            ViewBag.TransactionPrice = totalPrice;
            //To remove from the selected books to avoid adding them in selected items again and again
            selectedBooks.Clear();
            selectedCourses.Clear();
            return View(selectedItems);
        }

        public ActionResult CheckOut()
        {
            int count = 0;
            if (selectedItems.Count > 0)
            {
                foreach (ItemModel item in selectedItems)
                {
                    totalPrice = totalPrice + item.TotalPrice;

                }
            }

            count = dao.AddTransaction(Session.SessionID + count, DateTime.Now, totalPrice, Session["email"].ToString());

            if (selectedItems.Count > 0)
            {
                foreach (ItemModel item in selectedItems)
                {

                    dao.AddTransactionItem(Session.SessionID + count, item);
                }
            }
            count++;
            Session.Clear();
            //Session.Abandon();

            return View();
        }



    }
}