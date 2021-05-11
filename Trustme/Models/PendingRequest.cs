using System;
using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{
    public class PendingRequest
    {
        [Key]
        public int IdPedingUsers { get; set; }
        public string Username { get; set; }
        public DateTime TimePendingRequest { get; set; }
        public User User { get; set; }
    }
}
