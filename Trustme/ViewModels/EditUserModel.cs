using System.ComponentModel.DataAnnotations;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class EditUserModel
    {
        public EditUserModel()
        {

        }
        public EditUserModel(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            SecondName = user.SecondName;
            Mail = user.Mail;
            Username = user.Username;
            Role = user.Role;
            RoleId = user.RoleId;
        }
        public int UserId { set; get; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain just letters")]
        [Display(Name = "First Name")]

        public string FirstName { set; get; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain just letters")]
        [Display(Name = "Second Name")]
        public string SecondName { set; get; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "This is not a valid mail")]
        [Display(Name = "Mail")]
        public string Mail { set; get; }

        [Required]
        [RegularExpression("^(?!.{26})[a-zA-Z0-9]+?$", ErrorMessage = "In username field is allowing just letters or numbers")]
        [Display(Name = "Username")]
        public string Username { set; get; }
        public int RoleId { get; set; }

        public Role Role { get; set; }


    }
}
