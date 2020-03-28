using System.ComponentModel.DataAnnotations;

namespace ThriveWebApp.Models
{
    public class Contact
    {
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Comments { get; set; }
    }
}