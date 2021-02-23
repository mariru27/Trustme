using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Data;
using Trustme.Models;
using Trustme.IServices;

namespace Trustme.Service
{
    public class UnsignedDocumentRepository : IUnsignedDocumentRepository
    {
        private AppContext _context;
        public UnsignedDocumentRepository(AppContext context)
        {
            _context = context;
        }
        public void AddUnsignedDocument(UserUnsignedDocument userUnsignedDocument)
        {

            _context.UnsignedDocuments.Add(userUnsignedDocument.UnsignedDocument);
            _context.UserUnsignedDocuments.Add(userUnsignedDocument);
            _context.SaveChanges();
        }

        public UnsignedDocument GetUnsignedDocumentById(int IdUnsignedDocument)
        {
            _context.UnsignedDocuments.Where(u => u.IdUnsignedDocument == IdUnsignedDocument).SingleOrDefault();
            throw new System.NotImplementedException();
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
