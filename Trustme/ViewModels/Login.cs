using System.ComponentModel.DataAnnotations;

namespace Trustme.ViewModels
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
