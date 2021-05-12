using System;
using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{
    public class Pending
    {
        public Pending()
        {
            TimePendingRequest = DateTime.Now;
            Accepted = false;
        }

        [Key]
        public int IdPedingUsers { get; set; }
        public string Username { get; set; }
        public bool Accepted { get; set; }
        public DateTime TimePendingRequest { get; set; }
        public User User { get; set; }
    }
}
