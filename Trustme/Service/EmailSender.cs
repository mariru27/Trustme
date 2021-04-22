using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Trustme.IServices;

namespace Trustme.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _Configuration;
        public EmailSender(IConfiguration iConfig)
        {
            _Configuration = iConfig;
        }
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

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
            bodyBuilder.TextBody = "Hello World!";

            //Connect and authenticate with the SMTP server
            SmtpClient client = new SmtpClient();
            client.Connect("timetrustme99@gmail.com", 587, true);

            //email password is stored in my local.json file
            client.Authenticate("timetrustme99@gmail.com", _Configuration.GetSection("SMTPpassword").Value);
        }
    }
}
