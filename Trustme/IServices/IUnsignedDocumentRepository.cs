using System.Collections.Generic;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.IServices
{
    public interface IUnsignedDocumentRepository
    {
        public void AddUnsignedDocument(UnsignedDocumentUserKey unsignedDocumentUserKey);
        public IEnumerable<UnsignedDocument> ListAllUsignedDocumentsByUser(User User);
        public IEnumerable<UnsignedDocument> ListAllSignedDocumentsByUser(User User);

        public UnsignedDocument GetUnsignedDocumentById(int IdUnsignedDocument);
        public UnsignedDocument GetUnsignedDocumentByUserDocumentName(User user, string unsignedDocumentName);
        public UnsignedDocument MakeDocumentSigned(UnsignedDocument unsignedDocument);
        public void DeleteUnsignedDocument(int IdUnsignedDocument);

    }
}
