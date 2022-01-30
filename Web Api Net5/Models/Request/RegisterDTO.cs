using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Api_Net5.Models
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmationPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Phone]
        public string PhoneNumber { get; set; }

    }
}