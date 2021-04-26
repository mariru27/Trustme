using System.Collections.Generic;
using Trustme.Models;

namespace Trustme.IServices
{
    public interface ISignedDocumentRepository
    {
        public bool AddSignedDocument(SignedDocument signedDocument, User user);
        public IEnumerable<SignedDocument> ListAllSignedDocuments(User user);
        public SignedDocument GetSignedDocumentById(int IdSignedDocument);
        public void DeleteSignedDocument(int IdSignedDocument);
        public IEnumerable<SignedDocument> Search_ListAllSignedDocumentsSentFromUsername(User user, string Username);
        public IEnumerable<SignedDocument> Search_ListAllSignedDocumentsSignedByUsername(User user, string Username);
    }
}
