using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class UnsignedDocument
    {
        public UnsignedDocument() { }
        public UnsignedDocument(UnsignedDocument unsignedDocument)
        {
            this.IdUnsignedDocument = unsignedDocument.IdUnsignedDocument;
            this.KeyPreference = unsignedDocument.KeyPreference;
            this.Name = unsignedDocument.Name;
            this.Document = unsignedDocument.Document;
            this.KeyId = unsignedDocument.KeyId;
            this.Key = unsignedDocument.Key;
        }

        [Key]
        public int IdUnsignedDocument { get; set; }
        public string Name { get; set; }
        public byte[] Document { get; set; }
        public string KeyPreference { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }

    }
}
