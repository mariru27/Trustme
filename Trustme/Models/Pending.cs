using System;
using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{
    public class Pending
    {
        public Pending()
        {
            TimePendingRequest = DateTime.Now;
        }

        [Key]
        public int IdPedingUsers { get; set; }
        public string Username { get; set; }
        public string Accepted { get; set; }
        public DateTime TimePendingRequest { get; set; }
        public User User { get; set; }
    }
}
