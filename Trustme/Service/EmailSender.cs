using MailKit.Net.Smtp;
using MimeKit;
using System;
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

        public bool SendMail(SendMailModel sendMailModel)
        {
            try
            {

                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress(_NotificationMetadata.FromUsername,
                                        _NotificationMetadata.Sender);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress(sendMailModel.ToUsername,
                                        sendMailModel.ToUserMail);
                message.To.Add(to);

                message.Subject = sendMailModel.MessageSubject;

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h5>" + sendMailModel.MessageBodyHtml + "</h5>";
                bodyBuilder.TextBody = sendMailModel.MessageBodyContent;
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
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
