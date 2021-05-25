using System;
using System.ComponentModel.DataAnnotations;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class UnsignedDocumentView
    {
        public UnsignedDocumentView(UnsignedDocument unsignedDocument)
        {
            this.IdUnsignedDocument = unsignedDocument.IdUnsignedDocument;
            this.KeyPreference = unsignedDocument.KeyPreference;
            this.Name = unsignedDocument.Name;
            this.KeyId = unsignedDocument.KeyId;
            this.Key = unsignedDocument.Key;
            this.Signed = unsignedDocument.Signed;
            this.SentFromUsername = unsignedDocument.SentFromUsername;
            this.SentTime = unsignedDocument.SentTime;
            this.Seen = unsignedDocument.Seen;
            this.Show = unsignedDocument.Show;
        }

        [Key]
        public int IdUnsignedDocument { get; set; }
        public string Name { get; set; }
        public string SentFromUsername { get; set; }
        public string KeyPreference { get; set; }
        public bool Seen { get; set; }
        public bool Show { get; set; }
        public bool Signed { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }
        public DateTime SentTime { get; set; }
    }
}
