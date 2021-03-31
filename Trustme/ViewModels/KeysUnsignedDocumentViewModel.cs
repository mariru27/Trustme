using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class KeysUnsignedDocumentViewModel
    {
        public IEnumerable<Key> Keys { get; set; }
        public Key Key { get; set; }
        public UnsignedDocument UnsignedDocument { get; set; }
        [Required]
        [Display(Name = "Private Key")]
        public IFormFile PkFile { get; set; }
        public string Signature { get; set; }
        public int IdUnsignedDocument { get; set; }

    }
}
