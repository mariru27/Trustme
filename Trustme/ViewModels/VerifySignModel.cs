using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class VerifySignModel
    {
        [Required]
        public string Username { get; set; }
        public List<Key> Keys { get; set; }
        [Required]
        public string CertificateName { get; set; }
        [Required]
        public string Signature { get; set; }
        [Required]
        public IFormFile Document { get; set; }

    }
}
