using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
