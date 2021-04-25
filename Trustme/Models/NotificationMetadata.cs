namespace Trustme.Models
{
    public class NotificationMetadata
    {
        public string Sender { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string FromUsername { get; set; }
        public string Password { get; set; }

    }
}
