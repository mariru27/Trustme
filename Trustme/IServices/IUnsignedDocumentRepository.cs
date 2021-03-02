using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.IServices
{
    public interface IUnsignedDocumentRepository
    {
        public void AddUnsignedDocument(UnsignedDocumentUserKey unsignedDocumentUserKey);
        public IEnumerable<UnsignedDocument> ListAllUsignedDocumentsByUser(User User);
        public UnsignedDocument GetUnsignedDocumentById(int IdUnsignedDocument);
        public UnsignedDocument GetUnsignedDocumentByUserDocumentName(User user, string unsignedDocumentName)

    }
}
