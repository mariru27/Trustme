using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.IServices
{
    public interface IUnsignedDocumentRepository
    {
        public void AddUnsignedDocument(UserUnsignedDocument userUnsignedDocument);
        public IEnumerable<UnsignedDocument> ListAllUsignedDocumentsByUser(User User);

        public UnsignedDocument GetUnsignedDocumentById(int IdUnsignedDocument);
    }
}
