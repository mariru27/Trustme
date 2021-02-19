using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class SignedDocument
    {
        public int IdSignedDocument { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }
        public IFormFile Document { get; set; }
        public string Signature { get; set; }
        public string Comment { get; set; }
    }
}
