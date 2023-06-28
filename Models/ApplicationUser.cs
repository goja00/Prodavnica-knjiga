using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel;

namespace bookverse.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }  
        
        public string? Address { get; set; }
        public string? Town { get; set; }
        [DisplayName("Zip code")]
        public int? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Comment { get; set; }

    }
}
