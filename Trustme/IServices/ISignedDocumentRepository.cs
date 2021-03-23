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

    }
}
