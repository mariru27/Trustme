using MimeKit;
using Trustme.IServices;

namespace Trustme.Service
{
    public class EmailSender : IEmailSender
    {
        public void SendMail(string messageContent, string toUserName)
        {
            MimeMessage message = new MimeMessage();

        };
    }
}
