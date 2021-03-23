using System;
using Trustme.Models;

//I use this model to pass data from controller to view,
//Here I do not use document property
//(document file will slow data transfer to view)

namespace Trustme.ViewModels
{
    public class SignedDocumentsViewModel
    {
        public SignedDocumentsViewModel()
        {

        }
        public SignedDocumentsViewModel(SignedDocument signedDocument)
        {
            this.IdSignedDocument = signedDocument.IdSignedDocument;
            this.KeyId = signedDocument.KeyId;
            this.Key = signedDocument.Key;
            this.Signature = signedDocument.Signature;
            this.SignedByUsername = signedDocument.SignedByUsername;
            this.SentFromUsername = signedDocument.SentFromUsername;
            this.Name = signedDocument.Name;
            this.SignedTime = signedDocument.SignedTime;
            this.SentTime = signedDocument.SentTime;
        }
        public int IdSignedDocument { get; set; }
        public string Name { get; set; }
        public string SignedByUsername { get; set; }
        public string SentFromUsername { get; set; }
        public string Signature { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }
        public DateTime SignedTime { get; set; }
        public DateTime SentTime { get; set; }

    }
}
