using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Trustme.IServices;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    [Authorize(Roles = "Pro, Free")]

    public class SignedDocumentsController : Controller
    {
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        private readonly ISignedDocumentRepository _SignedDocumentRepository;
        public SignedDocumentsController(ISignedDocumentRepository signedDocumentRepository, IHttpRequestFunctions httpRequestFunctions)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            _SignedDocumentRepository = signedDocumentRepository;
        }

        [HttpGet]
        public IActionResult DeleteDocument(int id)
        {
            _SignedDocumentRepository.DeleteSignedDocument(id);
            return RedirectToAction("SignedDocumentsFromUsers");
        }


        public IActionResult SignedDocumentsFromUsers()
        {
            //Get all users signedDocuments
            IEnumerable<SignedDocument> signedDocuments = _SignedDocumentRepository.ListAllSignedDocuments(_HttpRequestFunctions.GetUser(HttpContext));
            if (signedDocuments.Count() == 0)
            {
                TempData["DoNotHaveAnySignedDocuments"] = "You do not have any signed documents";
            }
            List<SignedDocumentsViewModel> signedDocumentsViewModels = new List<SignedDocumentsViewModel>();

            //Cast SignedDocument to SignedDocumentsViewModel
            foreach (var doc in signedDocuments)
            {
                SignedDocumentsViewModel signedDocumentsViewModel = new SignedDocumentsViewModel(doc);
                signedDocumentsViewModels.Add(signedDocumentsViewModel);

            }
            return View(signedDocumentsViewModels);

        }

    }
}
