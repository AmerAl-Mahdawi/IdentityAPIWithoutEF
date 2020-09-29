using IdentityAPIWithoutEF.Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace IdentityAPIWithoutEF.Models
{
    public class LoginApiModel
    {
        [Required]
        [ValidateEmail(ErrorMessage = "Email is not valid")] // This is a customized attribute
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
