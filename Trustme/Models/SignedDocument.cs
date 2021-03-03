using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.Models
{
    public class SignedDocument
    {
        public SignedDocument() { }
        public SignedDocument(SignedDocument signedDocument)
        {
            this.Comment = signedDocument.Comment;
            this.Document = signedDocument.Document;
            this.IdSignedDocument = signedDocument.IdSignedDocument;
            this.Key = signedDocument.Key;
            this.KeyId = signedDocument.KeyId;
            this.Name = signedDocument.Name;
            this.Signature = signedDocument.Signature;
        }

        public SignedDocument(UnsignedDocument unsignedDocument, string Signature)
        {
            this.Key = unsignedDocument.Key;
            this.KeyId = unsignedDocument.KeyId;
            this.Name = unsignedDocument.Name;
            this.Signature = Signature;
            this.Document = unsignedDocument.Document;
        }

        [Key]
        public int IdSignedDocument { get; set; }
        public string Name { get; set; }
        public byte[] Document { get; set; }
        public string Signature { get; set; }
        public string Comment { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }
    }
}
