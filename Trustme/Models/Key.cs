using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class Key
    {
        [Key]
        public int KeyId { set; get; }
        public int UserId { set; get; }
        public string PublicKey { set; get; }
        public virtual User User { set; get; }
    }
}
