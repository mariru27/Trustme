using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class UploadDocumentModel
    {
        public IEnumerable<Key> Keys { get; set; }
        public string Username { get; set; }
        [Required]
        public string CertificateName { get; set; }
        [Required]
        public IFormFile Document { get; set; }
    }
}
