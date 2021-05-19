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
        private readonly IKeyRepository _KeyRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IPendingRepository _PendingRepository;

        public UnsignedDocumentRepository(AppContext context, IPendingRepository pendingRepository, IKeyRepository keyRepository, IUserRepository userRepository)
        {
            _KeyRepository = keyRepository;
            _UserRepository = userRepository;
            _PendingRepository = pendingRepository;
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
            //get all accepted users, pending requests
            var pendingRequsts = GetPedingsAcceptedByUser(user);


            //get unsigned documents just from accepted users(panding)
            IEnumerable<UnsignedDocument> allAcceptedUnsignedDocument = Enumerable.Empty<UnsignedDocument>();
            foreach (var peding in pendingRequsts)
            {
                IEnumerable<UnsignedDocument> acceptedUnsignedDocuments = _context.UserUnsignedDocuments.AsNoTracking().Where(u => u.UserId == user.UserId).Join(
                    _context.UnsignedDocuments,
                    u => u.UnsignedDocumentId,
                    ud => ud.IdUnsignedDocument,
                    (u, ud) => new UnsignedDocument(ud)).ToList().Where(a => a.Signed == false && a.SentFromUsername == peding.UsernameWhoSentPending).ToList();
                allAcceptedUnsignedDocument = allAcceptedUnsignedDocument.Union(acceptedUnsignedDocuments);
            }

            return allAcceptedUnsignedDocument.OrderByDescending(a => a.SentTime).ToList();
        }


        public IEnumerable<UnsignedDocument> ListAllSignedDocumentsByUser(User user)
        {

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
            (u, ud) => new UnsignedDocument(ud)).ToList().Where(a => a.Signed == false && a.Seen == false).ToList();

            foreach (var u in unsignedDocuments)
            {
                u.Seen = true;
                _context.Update(u);
            }
            _context.SaveChanges();
        }
        public int CountSeen(User user)
        {
            //get all accepted users, pending requests
            var pendingRequsts = GetPedingsAcceptedByUser(user);

            //get unsigned documents just from accepted users(panding)
            IEnumerable<UnsignedDocument> allAcceptedUnsignedDocument = Enumerable.Empty<UnsignedDocument>();
            foreach (var peding in pendingRequsts)
            {

                IEnumerable<UnsignedDocument> acceptedUnsignedDocuments = _context.UserUnsignedDocuments.AsNoTracking().Where(u => u.UserId == user.UserId).Join(
                    _context.UnsignedDocuments,
                    u => u.UnsignedDocumentId,
                    ud => ud.IdUnsignedDocument,
                    (u, ud) => new UnsignedDocument(ud)).ToList().Where(a => a.Signed == false && a.SentFromUsername == peding.UsernameWhoSentPending && a.Seen == false).ToList();
                allAcceptedUnsignedDocument = allAcceptedUnsignedDocument.Union(acceptedUnsignedDocuments);
            }

            allAcceptedUnsignedDocument = allAcceptedUnsignedDocument.OrderByDescending(a => a.SentTime).ToList();

            return allAcceptedUnsignedDocument.Count();
        }
        private IEnumerable<Pending> GetPedingsAcceptedByUser(User user)
        {
            //get all accepted users, pending requests
            return _context.User.AsNoTracking().Where(a => a.UserId == user.UserId).Join(_context.Pendings,
                u => u.UserId,
                p => p.User.UserId,
                (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers, TimeAcceptedPendingRequest = p.TimeAcceptedPendingRequest, Accepted = p.Accepted, Blocked = p.Blocked })
                .ToList().Where(a => a.Accepted == true).ToList();
        }
    }
}
