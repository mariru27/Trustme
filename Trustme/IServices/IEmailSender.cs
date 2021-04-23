using Trustme.Models;

namespace Trustme.IServices
{
    public interface IEmailSender
    {
        public void SendMail(SendMailModel sendMailModel);

    }
}
