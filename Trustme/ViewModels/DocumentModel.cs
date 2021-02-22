using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class DocumentModel
    {
        public string Username;
        public string CertificateName;
        public IFormFile Document;

    }
}
