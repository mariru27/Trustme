using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{
    public class UserKey
    {
        public UserKey()
        {
            Keys = new List<Key>();
        }
        [Key]
        public int IdUserKey { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        //public Key Key { get; set; }
        public ICollection<Key> Keys { get; set; }
    }
}
