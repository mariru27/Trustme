using System.Collections.Generic;
using System.Linq;
using Trustme.Data;
using Trustme.IServices;
using Trustme.Models;

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
            signedDocuments = signedDocuments.OrderByDescending(n => n.SignedTime).ToList();
            return signedDocuments;
        }
        public IEnumerable<SignedDocument> Search_ListAllSignedDocumentsSignedByUsername(User user, string Username)
        {
            IEnumerable<SignedDocument> signedDocuments = _context.UserSignedDocuments.Where(u => u.UserId == user.UserId).Join(
            _context.SignedDocuments,
            u => u.SignedDocumentId,
            ud => ud.IdSignedDocument,
            (u, ud) => new SignedDocument(ud)).ToList();
            signedDocuments = signedDocuments.Where(a => a.SignedByUsername == Username).ToList();

            if (signedDocuments == null)
                return null;
            signedDocuments = signedDocuments.OrderByDescending(n => n.SignedTime).ToList();
            return signedDocuments;
        }

        public IEnumerable<SignedDocument> Search_ListAllSignedDocumentsSentFromUsername(User user, string Username)
        {
            IEnumerable<SignedDocument> signedDocuments = _context.UserSignedDocuments.Where(u => u.UserId == user.UserId).Join(
            _context.SignedDocuments,
            u => u.SignedDocumentId,
            ud => ud.IdSignedDocument,
            (u, ud) => new SignedDocument(ud)).ToList();
            signedDocuments = signedDocuments.Where(a => a.SentFromUsername == Username).ToList();

            if (signedDocuments == null)
                return null;
            signedDocuments = signedDocuments.OrderByDescending(n => n.SignedTime).ToList();
            return signedDocuments;
        }
        public IEnumerable<SignedDocument> Search_ListAllSignedDocumentsSentFromUsername_SignedByUsername(User user, string SentFromUsername, string SignedByUsername)
        {
            IEnumerable<SignedDocument> signedDocuments = _context.UserSignedDocuments.Where(u => u.UserId == user.UserId).Join(
            _context.SignedDocuments,
            u => u.SignedDocumentId,
            ud => ud.IdSignedDocument,
            (u, ud) => new SignedDocument(ud)).ToList();
            signedDocuments = signedDocuments.Where(a => a.SentFromUsername == SentFromUsername && a.SignedByUsername == SignedByUsername).ToList();

            if (signedDocuments == null)
                return null;
            signedDocuments = signedDocuments.OrderByDescending(n => n.SignedTime).ToList();

            return signedDocuments;
        }
        public void DeleteSignedDocument(int id)
        {
            _context.SignedDocuments.Remove(this.GetSignedDocumentById(id));
            _context.SaveChanges();
        }

        public SignedDocument GetSignedDocumentById(int IdSignedDocument)
        {
            return _context.SignedDocuments.Where(d => d.IdSignedDocument == IdSignedDocument).SingleOrDefault();
        }

        public void MakeSeen(User user)
        {
            IEnumerable<SignedDocument> signedDocuments = _context.UserSignedDocuments.Where(u => u.UserId == user.UserId).Join(
            _context.SignedDocuments,
            u => u.SignedDocumentId,
            ud => ud.IdSignedDocument,
            (u, ud) => new SignedDocument(ud)).ToList();
            signedDocuments = signedDocuments.OrderByDescending(n => n.SignedTime).ToList().Where(s => s.Seen == false);

            foreach (var s in signedDocuments)
            {
                s.Seen = true;
                _context.Update(s);
            }
            _context.SaveChanges();
        }

        public int CountSeen(User user)
        {
            IEnumerable<SignedDocument> signedDocuments = _context.UserSignedDocuments.Where(u => u.UserId == user.UserId).Join(
            _context.SignedDocuments,
            u => u.SignedDocumentId,
            ud => ud.IdSignedDocument,
            (u, ud) => new SignedDocument(ud)).ToList();
            signedDocuments = signedDocuments.OrderByDescending(n => n.SignedTime).ToList().Where(s => s.Seen == false);
            return signedDocuments.Count();
        }

    }
}
