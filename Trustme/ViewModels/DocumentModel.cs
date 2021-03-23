using Microsoft.AspNetCore.Http;

namespace Trustme.ViewModels
{
    public class DocumentModel
    {
        public string Username;
        public string CertificateName;
        public IFormFile Document;

    }
}
