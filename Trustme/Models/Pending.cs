using System;
using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{
    public class Pending
    {
        public Pending()
        {
            TimeSentPendingRequest = DateTime.Now;
            Accepted = false;
        }

        [Key]
        public int IdPedingUsers { get; set; }
        public string UsernameWhoSentPending { get; set; }
        public bool Accepted { get; set; }
        public DateTime TimeSentPendingRequest { get; set; }
        public DateTime TimeAcceptedPendingRequest { get; set; }
        public User User { get; set; }
    }
}
