﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IKeyRepository _KeyRepository;
        public SignedDocumentsController(IEmailSender emailSender, IKeyRepository keyRepository, ISignedDocumentRepository signedDocumentRepository, IHttpRequestFunctions httpRequestFunctions)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            _SignedDocumentRepository = signedDocumentRepository;
            _KeyRepository = keyRepository;
        }

        [HttpGet]
        public IActionResult DeleteDocument(int id)
        {
            _SignedDocumentRepository.DeleteSignedDocument(id);
            return RedirectToAction("SignedDocumentsFromUsers");
        }

        public List<SignedDocumentsViewModel> Cast_SignedDocumentToSignedDocumentsViewModel(IEnumerable<SignedDocument> signedDocuments)
        {
            if (signedDocuments.Count() == 0)
            {
                TempData["DoNotHaveAnySignedDocuments"] = "You do not have any signed documents";
            }
            List<SignedDocumentsViewModel> signedDocumentsViewModels = new List<SignedDocumentsViewModel>();

            //Cast SignedDocument to SignedDocumentsViewModel
            foreach (var doc in signedDocuments)
            {
                SignedDocumentsViewModel signedDocumentsViewModel = new SignedDocumentsViewModel(doc);
                signedDocumentsViewModel.KeyName = _KeyRepository.GetKeyById(doc.KeyId).CertificateName;
                signedDocumentsViewModels.Add(signedDocumentsViewModel);

            }

            return signedDocumentsViewModels;
        }

        [HttpGet]
        public IActionResult Search(string SignedByUsername, string SentFromUsername)
        {
            User user = _HttpRequestFunctions.GetUser(HttpContext);
            if (SignedByUsername != null && SentFromUsername != null)
            {
                var signedDocuments = _SignedDocumentRepository.Search_ListAllSignedDocumentsSentFromUsername_SignedByUsername
                                      (user, SentFromUsername, SignedByUsername);
                List<SignedDocumentsViewModel> signedDocumentsViewModels = Cast_SignedDocumentToSignedDocumentsViewModel(signedDocuments);
                return View("SignedDocumentsFromUsers", signedDocumentsViewModels);

            }
            else
            {
                if (SignedByUsername != null)
                {
                    var signedDocuments = _SignedDocumentRepository.Search_ListAllSignedDocumentsSignedByUsername(user, SignedByUsername);
                    List<SignedDocumentsViewModel> signedDocumentsViewModels = Cast_SignedDocumentToSignedDocumentsViewModel(signedDocuments);
                    return View("SignedDocumentsFromUsers", signedDocumentsViewModels);
                }

                if (SentFromUsername != null)
                {
                    var signedDocuments = _SignedDocumentRepository.Search_ListAllSignedDocumentsSentFromUsername(user, SentFromUsername);
                    List<SignedDocumentsViewModel> signedDocumentsViewModels = Cast_SignedDocumentToSignedDocumentsViewModel(signedDocuments);
                    return View("SignedDocumentsFromUsers", signedDocumentsViewModels);
                }
            }
            return RedirectToAction("SignedDocumentsFromUsers");
        }

        public IActionResult SignedDocumentDetails(int id)
        {
            SignedDocument signedDocument = _SignedDocumentRepository.GetSignedDocumentById(id);
            SignedDocumentsViewModel signedDocumentsViewModel = new SignedDocumentsViewModel(signedDocument);
            signedDocumentsViewModel.KeyName = _KeyRepository.GetKeyById(signedDocument.KeyId).CertificateName;
            return View(signedDocumentsViewModel);
        }
        public IActionResult Download(int id)
        {
            var document = _SignedDocumentRepository.GetSignedDocumentById(id);
            return File(document.Document, document.ContentType, document.Name);
        }

        public IActionResult SignedDocumentsFromUsers()
        {
            //Get user
            User user = _HttpRequestFunctions.GetUser(HttpContext);
            //Get all users signedDocuments
            IEnumerable<SignedDocument> signedDocuments = _SignedDocumentRepository.ListAllSignedDocuments(user);

            List<SignedDocumentsViewModel> signedDocumentsViewModels = Cast_SignedDocumentToSignedDocumentsViewModel(signedDocuments);

            _SignedDocumentRepository.MakeSeen(user);

            return View(signedDocumentsViewModels);

        }

    }
}
