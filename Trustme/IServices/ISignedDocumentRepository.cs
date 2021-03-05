using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.IServices
{
    public interface ISignedDocumentRepository
    {
        public bool AddSignedDocument(SignedDocument signedDocument, User user);
        public IEnumerable<SignedDocument> ListAllSignedDocuments(User user);

    }
}
