using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class SignedDocument
    {
        [Key]
        public int IdSignedDocument { get; set; }
        public string Name { get; set; }
        public byte[] Document { get; set; }
        public string Signature { get; set; }
        public string Comment { get; set; }

    }
}
