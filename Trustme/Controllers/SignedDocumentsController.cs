﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.IServices;
using Trustme.Service;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
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

        
        public IActionResult SignedDocumentsForUser()
        {
            User currentUser = _HttpRequestFunctions.GetUser(HttpContext);
            IEnumerable<SignedDocument> signedDocuments = _SignedDocumentRepository.ListAllSignedDocuments(currentUser);
            return View();

        }

    }
}
