using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTemplate.Models
{
    public class Book1
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public byte[] BookImage { get; set; }

        public Book1() { }
        public Book1(string isbn, string title, string publisher, DateTime date, decimal price, byte[] image)
        {
            ISBN = isbn;
            Title = title;
            Publisher = publisher;
            PublicationDate = date;
            Price = price;
            BookImage = image;
        }
    }
}
