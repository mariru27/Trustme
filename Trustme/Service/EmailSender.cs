using MimeKit;
using Trustme.IServices;

namespace Trustme.Service
{
    public class EmailSender : IEmailSender
    {
        public void SendMail(string messageContent, string toUserNameMail)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin",
                                    "timetrustme99@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("user",
                                    "marina.rusu.99@gmail.com");
            message.To.Add(to);

            message.Subject = "This is email test subject";

        }
    }
}
