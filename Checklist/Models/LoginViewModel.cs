using System.ComponentModel.DataAnnotations;

namespace Checklist.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email address")]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
