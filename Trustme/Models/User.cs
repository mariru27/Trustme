using System.ComponentModel.DataAnnotations;
using Trustme.ViewModels;

namespace Trustme.Models
{

    public class User
    {
        [Key]
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

        [Required]
        //[StringLength(20, MinimumLength = 5, ErrorMessage = "Password must have more then 5 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, type again !")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Role")]

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public void Update(EditUserModel user)
        {
            Username = user.Username;
            FirstName = user.FirstName;
            SecondName = user.SecondName;
            Mail = user.Mail;
            RoleId = user.RoleId;
        }

    }
}
