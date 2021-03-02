using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Data;
using Trustme.Models;
using Trustme.IServices;
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

            UserUnsignedDocument userUnsignedDocument = new UserUnsignedDocument();
            userUnsignedDocument.UserId = unsignedDocumentUserKey.User.UserId;
            userUnsignedDocument.User = unsignedDocumentUserKey.User;
            userUnsignedDocument.UnsignedDocumentId = unsignedDocumentUserKey.UnsignedDocument.IdUnsignedDocument;
            
            _context.UserUnsignedDocuments.Add(userUnsignedDocument);
            
            _context.SaveChanges();
        }

        public UnsignedDocument GetUnsignedDocumentById(int IdUnsignedDocument)
        {
            return _context.UnsignedDocuments.Where(u => u.IdUnsignedDocument == IdUnsignedDocument).SingleOrDefault();
        }

        public UnsignedDocument GetUnsignedDocumentByUserDocumentName(User user, string unsignedDocumentName)
        {
            UnsignedDocument unsignedDocument = _context.UserUnsignedDocuments.Where(u => u.UserId == user.UserId).Join(
                _context.UnsignedDocuments,
                u => u.UnsignedDocumentId,
                d => d.IdUnsignedDocument,
                (u, d) => new UnsignedDocument()
                ).Where(a => a.Name == unsignedDocumentName).SingleOrDefault();
            return unsignedDocument;
        }
        public IEnumerable<UnsignedDocument> ListAllUsignedDocumentsByUser(User user)
        {
            IEnumerable<UnsignedDocument> unsignedDocuments = _context.UserUnsignedDocuments.Where(u => u.UserId == user.UserId).Join(
                _context.UnsignedDocuments,
                u => u.UnsignedDocumentId,
                ud => ud.IdUnsignedDocument,
                (u, ud) => new UnsignedDocument(ud)).ToList();
            return unsignedDocuments;
        }
    }
}
