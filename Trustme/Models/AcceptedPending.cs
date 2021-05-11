using System;
using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{
    public class AcceptedPending
    {
        [Key]
        public int IdAcceptedPending { get; set; }
        public string Username { get; set; }
        public DateTime TimeAcceptedPending { get; set; }
        public User User { get; set; }
    }
}
