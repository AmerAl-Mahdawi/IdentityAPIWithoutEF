using System.ComponentModel.DataAnnotations;

namespace IdentityAPIWithoutEF.Models
{
    public class RoleApiModel
    {
        [Required]
        public string Name { get; set; }
    }
}
