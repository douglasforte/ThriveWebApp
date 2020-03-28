using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleTemplate.Models
{
    public class Module
    {
        [Required]
        [Display(Name ="Module Code")]
        public string ModuleCode { get; set; }

        [Required]
        [Display(Name ="Module Title")]
        public string ModuleTitle { get; set; }

        [Required]
        [Display(Name ="Contact Hours")]
        public int Hours { get; set; }
        public List<Section> Syllabus { get; set; }

        [Display(Name ="Course Title")]
        public string CourseTitle { get; set; }
    }
}