using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.IServices;
using Trustme.Models;
using Trustme.Data;

namespace Trustme.Service
{
    public class SignedDocumentRepository : ISignedDocumentRepository
    {
        private AppContext _context;
        public SignedDocumentRepository(AppContext appContext)
        {
            _context = appContext;
        }
        public bool AddSignedDocument(SignedDocument signedDocument)
        {
            
            return true;
        }
    }
}
