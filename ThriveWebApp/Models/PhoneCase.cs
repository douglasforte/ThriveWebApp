
using System.ComponentModel.DataAnnotations;

namespace ThriveWebApp.Models
{
    public class PhoneCase
    {
        public ProductNameEnum PhoneCaseName { get; set; }

        public ProductModelEnum ProductModel { get; set; }

        public ProductQuantityEnum Quantity { get; set; }
    }
}