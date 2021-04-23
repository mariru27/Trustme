using MailKit.Net.Smtp;
using MimeKit;
using Trustme.IServices;
using Trustme.Models;

namespace Trustme.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly NotificationMetadata _NotificationMetadata;
        public EmailSender(NotificationMetadata notificationMetadata)
        {
            _NotificationMetadata = notificationMetadata;

        }

        public void SendMail(string toUsername, string toUserNameMail, string messageBodyHtmml = "", string messageBodyContent, string messageSubject = "Subject")
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress(_NotificationMetadata.FromUsername,
                                    _NotificationMetadata.Sender);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(toUsername,
                                    toUserNameMail);
            message.To.Add(to);

            message.Subject = messageSubject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<h5>" + messageBodyHtmml + "</h5>";
            bodyBuilder.TextBody = "Test!";
            message.Body = bodyBuilder.ToMessageBody();

            //Connect and authenticate with the SMTP server
            SmtpClient client = new SmtpClient();
            client.Connect(_NotificationMetadata.SmtpServer, _NotificationMetadata.Port, true);

            //email password is stored in my local.json file
            client.Authenticate(_NotificationMetadata.Sender, _NotificationMetadata.Password);
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
