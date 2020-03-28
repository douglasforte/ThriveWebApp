using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SampleTemplate.Models
{
    public class Course
    {
        [Required]
        [Display(Name ="Course Code")]
        public string CourseCode { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}