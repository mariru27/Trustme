using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class UserKey
    {
        [Key]
        public int IdUserKey { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }
    }
}
