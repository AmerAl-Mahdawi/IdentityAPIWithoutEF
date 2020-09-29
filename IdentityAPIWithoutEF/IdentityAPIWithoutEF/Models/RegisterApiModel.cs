using IdentityAPIWithoutEF.Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPIWithoutEF.Models
{
    public class RegisterApiModel
    {
        [Required]
        [ValidateEmail(ErrorMessage = "Email is not valid")] // This is a customized attribute
        public string Email { get; set; }
        //public bool EmailConfirmed { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
    }
}
