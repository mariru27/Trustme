using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.ViewModels
{
    public class VerifySignatureDocumentModel
    {
        public string Username { get; set; }
        public string CertificateName { get; set; }
        public string Signature { get; set; }
        public IFormFile Document { get; set; }
    }
}
