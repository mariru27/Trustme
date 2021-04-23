﻿using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Trustme.IServices;
using Trustme.Models;

namespace Trustme.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _Configuration;
        private readonly NotificationMetadata _NotificationMetadata;
        public EmailSender(IConfiguration iConfig, NotificationMetadata notificationMetadata)
        {
            _Configuration = iConfig;
            _NotificationMetadata = notificationMetadata;

        }

        //public void SendMail(string messageContent, string toUserNameMail)
        public void SendMail()
        {
            MimeMessage message = new MimeMessage();
            var a = _NotificationMetadata.Sender;


            MailboxAddress from = new MailboxAddress("Admin",
                                    "timetrustme99@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("user",
                                    "marina.rusu.99@gmail.com");
            message.To.Add(to);

            message.Subject = "This is email test subject";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
            bodyBuilder.TextBody = "Hello World!";

            //Connect and authenticate with the SMTP server
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);

            //email password is stored in my local.json file
            client.Authenticate("timetrustme99@gmail.com", "");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}