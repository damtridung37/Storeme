using System.ComponentModel.DataAnnotations;

namespace Storeme.Web.Models.Identity
{
    public class LoginViewModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
