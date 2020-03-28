using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SampleTemplate.Models
{
    public class Section
    {
        [Required]
        public string Heading { get; set; }

        [Required]
        public string Description { get; set; }
    }
}