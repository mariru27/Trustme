using Microsoft.AspNetCore.Http;

namespace Trustme.Tools.ToolsModels
{
    public class VerifySignatureModel
    {
        public string Username { get; set; }
        public string CertificateName { get; set; }
        public IFormFile Document;
    }
}
