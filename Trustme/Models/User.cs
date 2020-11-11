using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
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
        public string firstName { set; get; }
        public string secondName { set; get; }
        public string mail { set; get; }
        public string username { set; get; }
        public string password { set; get; }
        public virtual Key Key {set; get;}
    }
}
