using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Trustme.IServices;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    [Authorize(Roles = "Pro, User, Free")]

    public class SignedDocumentsController : Controller
    {
        private IUnsignedDocumentRepository _UnsignedDocumentRepository;
        private IHttpRequestFunctions _HttpRequestFunctions;
        private ISignedDocumentRepository _SignedDocumentRepository;
        public SignedDocumentsController(ISignedDocumentRepository signedDocumentRepository, IUnsignedDocumentRepository unsignedDocumentRepository, IHttpRequestFunctions httpRequestFunctions)
        {
            _UnsignedDocumentRepository = unsignedDocumentRepository;
            _HttpRequestFunctions = httpRequestFunctions;
            _SignedDocumentRepository = signedDocumentRepository;
        }
        public IActionResult SignedDocumentsHistory()
        {
            IEnumerable<UnsignedDocument> signedDocuments = _UnsignedDocumentRepository.ListAllSignedDocumentsByUser(_HttpRequestFunctions.GetUser(HttpContext));
            return View(signedDocuments);
        }

        [HttpGet]

        public IActionResult DeleteDocument(int id)
        {
            _SignedDocumentRepository.DeleteSignedDocument(id);
            return RedirectToAction("SignedDocumentsFromUsers");
        }


        public IActionResult SignedDocumentsFromUsers()
        {


            User currentUser = _HttpRequestFunctions.GetUser(HttpContext);

            //Get all users signedDocuments
            IEnumerable<SignedDocument> signedDocuments = _SignedDocumentRepository.ListAllSignedDocuments(currentUser);
            List<SignedDocumentsViewModel> signedDocumentsViewModels = new List<SignedDocumentsViewModel>();

            //Cast SignedDocument to SignedDocumentsViewModel
            foreach(var doc in signedDocuments)
            {
                SignedDocumentsViewModel signedDocumentsViewModel = new SignedDocumentsViewModel(doc);
                signedDocumentsViewModels.Add(signedDocumentsViewModel);

            }
            return View(signedDocumentsViewModels);

        }

    }
}
