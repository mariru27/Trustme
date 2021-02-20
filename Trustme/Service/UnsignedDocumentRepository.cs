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
    }
}
