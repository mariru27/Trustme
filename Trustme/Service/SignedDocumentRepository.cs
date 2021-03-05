using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.IServices;
using Trustme.Models;
using Trustme.Data;
using Microsoft.AspNetCore.Http;
using Trustme.Service;

namespace Trustme.Service
{
    public class SignedDocumentRepository : ISignedDocumentRepository
    {
        private AppContext _context;
        private IHttpRequestFunctions _HttpRequestFunctions;
        public SignedDocumentRepository(AppContext appContext, IHttpRequestFunctions httpRequestFunctions)
        {
            _context = appContext;
            _HttpRequestFunctions = httpRequestFunctions;
        }
        public bool AddSignedDocument(SignedDocument signedDocument, User user)
        {
            
            _context.SignedDocuments.Add(signedDocument);
            _context.SaveChanges();

            UserSignedDocument userSignedDocument = new UserSignedDocument
            {
                SignedDocument = signedDocument,
                SignedDocumentId = signedDocument.IdSignedDocument,
                User = user,
                UserId = user.UserId,
            };

            _context.UserSignedDocuments.Add(userSignedDocument);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<SignedDocument> ListAllSignedDocuments(User user)
        {
            IEnumerable<SignedDocument> signedDocuments = _context.UserSignedDocuments.Where(u => u.UserId == user.UserId).Join(
            _context.SignedDocuments,
            u => u.SignedDocumentId,
            ud => ud.IdSignedDocument,
            (u, ud) => new SignedDocument(ud)).ToList();
            return signedDocuments;
        }
    }
}
