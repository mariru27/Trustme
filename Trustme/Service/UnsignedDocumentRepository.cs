using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Trustme.Data;
using Trustme.IServices;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Service
{
    public class UnsignedDocumentRepository : IUnsignedDocumentRepository
    {
        private AppContext _context;
        private IKeyRepository _KeyRepository;
        private IUserRepository _UserRepository;


        public UnsignedDocumentRepository(AppContext context, IKeyRepository keyRepository, IUserRepository userRepository)
        {
            _KeyRepository = keyRepository;
            _UserRepository = userRepository;
            _context = context;
        }
        public void AddUnsignedDocument(UnsignedDocumentUserKey unsignedDocumentUserKey)
        {
            _context.UnsignedDocuments.Add(unsignedDocumentUserKey.UnsignedDocument);
            _context.SaveChanges();

            UserUnsignedDocument userUnsignedDocument = new UserUnsignedDocument();
            userUnsignedDocument.UserId = unsignedDocumentUserKey.User.UserId;
            userUnsignedDocument.User = unsignedDocumentUserKey.User;
            userUnsignedDocument.UnsignedDocument = unsignedDocumentUserKey.UnsignedDocument;
            userUnsignedDocument.UnsignedDocumentId = unsignedDocumentUserKey.UnsignedDocument.IdUnsignedDocument;

            _context.UserUnsignedDocuments.Add(userUnsignedDocument);
            _context.SaveChanges();
        }

        public void DeleteUnsignedDocument(int IdUnsignedDocument)
        {

            _context.UnsignedDocuments.Remove(this.GetUnsignedDocumentById(IdUnsignedDocument));
            _context.SaveChanges();
        }
        public UnsignedDocument GetUnsignedDocumentById(int IdUnsignedDocument)
        {
            return _context.UnsignedDocuments.Where(u => u.IdUnsignedDocument == IdUnsignedDocument).SingleOrDefault();
        }

        public UnsignedDocument GetUnsignedDocumentByUserDocumentName(User user, string unsignedDocumentName)
        {
            var unsignedDocument = _context.UserUnsignedDocuments.Where(u => u.UserId == user.UserId).Join(
                _context.UnsignedDocuments,
                u => u.UnsignedDocumentId,
                d => d.IdUnsignedDocument,
                (u, d) => new UnsignedDocument(d)
                ).Where(a => a.Name == unsignedDocumentName).SingleOrDefault();
            return unsignedDocument;
        }
        public IEnumerable<UnsignedDocument> ListAllUsignedDocumentsByUser(User user)
        {

            IEnumerable<UnsignedDocument> unsignedDocuments = _context.UserUnsignedDocuments.AsNoTracking().Where(u => u.UserId == user.UserId).Join(
                _context.UnsignedDocuments,
                u => u.UnsignedDocumentId,
                ud => ud.IdUnsignedDocument,
                (u, ud) => new UnsignedDocument(ud)).ToList().Where(a => a.Signed == false);
            unsignedDocuments = unsignedDocuments.OrderByDescending(a => a.SentTime).ToList();
            return unsignedDocuments;
        }


        public IEnumerable<UnsignedDocument> ListAllSignedDocumentsByUser(User user)
        {
            //get all accepted users, pending requests
            var pendingRequsts = _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
                u => u.UserId,
                p => p.User.UserId,
                (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers })
                .ToList().Where(a => a.Accepted);

            //get all unsigned documents
            IEnumerable<UnsignedDocument> unsignedDocuments = _context.UserUnsignedDocuments.Where(u => u.UserId == user.UserId).Join(
                _context.UnsignedDocuments,
                u => u.UnsignedDocumentId,
                ud => ud.IdUnsignedDocument,
                (u, ud) => new UnsignedDocument(ud)).ToList().Where(a => a.Signed == true);

            return unsignedDocuments;
        }

        public IEnumerable<UnsignedDocument> Search_ListAllUnsignedDocumentsDocumentsByUsername(User user, string UserName)
        {

            IEnumerable<UnsignedDocument> unsignedDocuments = _context.UserUnsignedDocuments.Where(u => u.UserId == user.UserId).Join(
                _context.UnsignedDocuments,
                u => u.UnsignedDocumentId,
                ud => ud.IdUnsignedDocument,
                (u, ud) => new UnsignedDocument(ud)).ToList().Where(a => a.Signed == false);
            unsignedDocuments = unsignedDocuments.Where(u => u.SentFromUsername == UserName).ToList();

            if (unsignedDocuments == null)
                return null;
            unsignedDocuments = unsignedDocuments.OrderByDescending(a => a.SentTime).ToList();
            return unsignedDocuments;
        }

        public UnsignedDocument MakeDocumentSigned(UnsignedDocument unsignedDocument)
        {
            unsignedDocument.Signed = true;
            _context.UnsignedDocuments.Update(unsignedDocument);
            _context.SaveChanges();
            return unsignedDocument;
        }

        public void MakeSeen(User user)
        {
            IEnumerable<UnsignedDocument> unsignedDocuments = _context.UserUnsignedDocuments.AsNoTracking().Where(u => u.UserId == user.UserId).Join(
            _context.UnsignedDocuments,
            u => u.UnsignedDocumentId,
            ud => ud.IdUnsignedDocument,
            (u, ud) => new UnsignedDocument(ud)).ToList().Where(a => a.Signed == false).ToList().Where(u => u.Seen == false);

            foreach (var u in unsignedDocuments)
            {
                u.Seen = true;
                _context.Update(u);
            }
            _context.SaveChanges();
        }
        public int CountSeen(User user)
        {
            IEnumerable<UnsignedDocument> unsignedDocuments = _context.UserUnsignedDocuments.AsNoTracking().Where(u => u.UserId == user.UserId).Join(
            _context.UnsignedDocuments,
            u => u.UnsignedDocumentId,
            ud => ud.IdUnsignedDocument,
            (u, ud) => new UnsignedDocument(ud)).ToList().Where(a => a.Signed == false).ToList().Where(u => u.Seen == false);
            return unsignedDocuments.Count();
        }

    }
}
