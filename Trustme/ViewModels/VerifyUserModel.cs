using System.ComponentModel.DataAnnotations;

namespace Trustme.ViewModels
{
    public class VerifyUserModel
    {
        [Required]
        public string Username { get; set; }
    }
}
