using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.IServices;
using Trustme.Service;
using Trustme.Models;

namespace Trustme.Controllers
{
    public class SignedDocumentsController : Controller
    {
        private IUnsignedDocumentRepository _UnsignedDocumentRepository;
        private IHttpRequestFunctions _HttpRequestFunctions;
        public SignedDocumentsController(IUnsignedDocumentRepository unsignedDocumentRepository, IHttpRequestFunctions httpRequestFunctions)
        {
            _UnsignedDocumentRepository = unsignedDocumentRepository;
            _HttpRequestFunctions = httpRequestFunctions;
        }
        public IActionResult SignedDocumentsHistory()
        {
            IEnumerable<UnsignedDocument> signedDocuments = _UnsignedDocumentRepository.ListAllSignedDocumentsByUser(_HttpRequestFunctions.GetUser(HttpContext));
            return View(signedDocuments);
        }

        public IActionResult SignedDocumentsForUser()
        {
            return View();
        }

    }
}
