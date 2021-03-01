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

            UnsignedDocument unsignedDocument = new UnsignedDocument();
            unsignedDocument = unsignedDocumentUserKey.UnsignedDocument;

            _context.UnsignedDocuments.Add(unsignedDocument);

            UserUnsignedDocument userUnsignedDocument = new UserUnsignedDocument();
            userUnsignedDocument.User = unsignedDocumentUserKey.User;
            userUnsignedDocument.UserId = unsignedDocumentUserKey.User.UserId;
            userUnsignedDocument.UnsignedDocument = unsignedDocument;
            _context.UserUnsignedDocuments.Add(userUnsignedDocument);
            //UnsignedDocument unsignedDocument = new UnsignedDocument(userUnsignedDocument.UnsignedDocument);
            _context.SaveChanges();
        }

        public UnsignedDocument GetUnsignedDocumentById(int IdUnsignedDocument)
        {
            return _context.UnsignedDocuments.Where(u => u.IdUnsignedDocument == IdUnsignedDocument).SingleOrDefault();
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
