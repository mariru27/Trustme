using Trustme.Models;

namespace Trustme.IServices
{
    public interface IEmailSender
    {
        public bool SendMail(SendMailModel sendMailModel);

    }
}
