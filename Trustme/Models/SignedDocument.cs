using System;
using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{

    public class SignedDocument
    {

        public SignedDocument()
        {
            this.SignedTime = DateTime.Now;
            this.Seen = false;
        }
        public SignedDocument(SignedDocument signedDocument)
        {
            this.Comment = signedDocument.Comment;
            this.Document = signedDocument.Document;
            this.IdSignedDocument = signedDocument.IdSignedDocument;
            this.Key = signedDocument.Key;
            this.KeyId = signedDocument.KeyId;
            this.Name = signedDocument.Name;
            this.Signature = signedDocument.Signature;
            this.SignedByUsername = signedDocument.SignedByUsername;
            this.SentFromUsername = signedDocument.SentFromUsername;
            this.SentTime = signedDocument.SentTime;
            this.SignedTime = DateTime.Now;
            this.ContentType = signedDocument.ContentType;
            this.Seen = signedDocument.Seen;

        }

        public SignedDocument(UnsignedDocument unsignedDocument, string Signature, string SignedByUsername)
        {
            this.Key = unsignedDocument.Key;
            this.KeyId = unsignedDocument.KeyId;
            this.Name = unsignedDocument.Name;
            this.Signature = Signature;
            this.Document = unsignedDocument.Document;
            this.SignedByUsername = SignedByUsername;
            this.SentFromUsername = unsignedDocument.SentFromUsername;
            this.SentTime = unsignedDocument.SentTime;
            this.SignedTime = DateTime.Now;
            this.ContentType = unsignedDocument.ContentType;
            this.Seen = false;
        }

        [Key]
        public int IdSignedDocument { get; set; }
        public string Name { get; set; }
        public string SentFromUsername { get; set; }
        public string ContentType { get; set; }
        public string SignedByUsername { get; set; }
        public bool Seen { get; set; }
        public byte[] Document { get; set; }
        public string Signature { get; set; }
        public string Comment { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }

        public DateTime SignedTime { get; set; }

        public DateTime SentTime { get; set; }
    }
}
