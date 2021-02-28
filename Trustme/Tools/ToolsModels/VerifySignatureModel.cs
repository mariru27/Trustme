using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Tools.ToolsModels
{
    public class VerifySignatureModel
    {
        public string Username { get; set; }
        public string CertificateName { get; set; }
        public IFormFile Document;
    }
}
