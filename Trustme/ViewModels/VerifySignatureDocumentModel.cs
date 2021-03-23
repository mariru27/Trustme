using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Trustme.ViewModels
{
    public class VerifySignatureDocumentModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string CertificateName { get; set; }
        [Required]
        public string Signature { get; set; }
        [Required]
        public IFormFile Document { get; set; }
    }
}
