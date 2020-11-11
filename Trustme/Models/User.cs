using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class User
    {
        [Key]
        public int UserId { set; get; }
        
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage ="Name must contain just letters")]
        public string firstName { set; get; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain just letters")]
        public string secondName { set; get; }
    
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage= "This is not a valid mail")]
        public string mail { set; get; }
        
        [Required]
        [RegularExpression("^(?!.{26})[a-zA-Z0-9]+?$", ErrorMessage = "In username field is allowing just letters or numbers")]
        public string username { set; get; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string password { set; get; }
        public virtual Key Key {set; get;}
    }
}
