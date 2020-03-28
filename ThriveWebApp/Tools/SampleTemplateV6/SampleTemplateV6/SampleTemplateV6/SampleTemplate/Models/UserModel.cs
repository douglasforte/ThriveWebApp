using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SampleTemplate.Models
{
    public class UserModel
    {
        [Required]
        [RegularExpression("[A-Za-z]+")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("[A-Za-z]+")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [StringLength(10,ErrorMessage ="Password must be 5 to 10 characters long", MinimumLength =5)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="User Type")]
        public Role UserRole { get; set; }
    }
}