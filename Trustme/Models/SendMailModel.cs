namespace Trustme.Models
{
    public class SendMailModel
    {
        public string ToUsername { get; set; }
        public string ToUserMail { get; set; }
        public string MessageBodyHtml { get; set; }
        public string MessageBodyContent { get; set; }
        public string MessageSubject { get; set; }
    }
}
