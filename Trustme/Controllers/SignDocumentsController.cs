﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trustme.IServices;
using Trustme.ITools;
using Trustme.Models;
using Trustme.Tools.ToolsModels;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    [Authorize(Roles = "Pro, Free")]

    public class SignDocumentsController : Controller
    {
        private readonly IKeyRepository _KeyRepository;
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        private readonly IUnsignedDocumentRepository _UnsignedDocumentRepository;
        private readonly ISign _Sign;
        private readonly IUserRepository _UserRepository;
        private readonly ISignedDocumentRepository _SignedDocumentRepository;
        public SignDocumentsController(IUserRepository userRepository, ISignedDocumentRepository signedDocumentRepository, IKeyRepository keyRepository, IHttpRequestFunctions httpRequestFunctions, IUnsignedDocumentRepository unsignedDocumentRepository, ISign sign)
        {
            _KeyRepository = keyRepository;
            _HttpRequestFunctions = httpRequestFunctions;
            _Sign = sign;
            _UnsignedDocumentRepository = unsignedDocumentRepository;
            _SignedDocumentRepository = signedDocumentRepository;
            _UserRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Search(string Username)
        {
            var unsignedDocuments = _UnsignedDocumentRepository.Search_ListAllUnsignedDocumentsDocumentsByUsername(_HttpRequestFunctions.GetUser(HttpContext), Username);
            if (unsignedDocuments.Any() == false)
            {
                TempData["SearchResult"] = "You do not have documents uploaded by username " + Username;
            }
            return View("UnsignedDocuments", unsignedDocuments);
        }
        public IActionResult UnsignedDocuments()
        {
            User user = _HttpRequestFunctions.GetUser(HttpContext);
            IEnumerable<UnsignedDocument> unsignedDocuments = _UnsignedDocumentRepository.ListAllUsignedDocumentsByUser(user);
            int countDelivered = _UnsignedDocumentRepository.CountDelivered(user);

            if (countDelivered != 0)
            {
                TempData["NrDelivered"] = countDelivered;
                _UnsignedDocumentRepository.MakeDelivered(user);
            }

            if (unsignedDocuments.Count() == 0)
                TempData["DoNotHaveAnyUnsignedDocuments"] = "You do not have any documents to sign. You can upload one if you already generated a key";

            return View(unsignedDocuments);
        }

        public IActionResult Download(int id)
        {
            var document = _UnsignedDocumentRepository.GetUnsignedDocumentById(id);
            return File(document.Document, document.ContentType, document.Name);
        }

        [HttpGet]
        public IActionResult DeleteDocument(int id)
        {
            _UnsignedDocumentRepository.DeleteUnsignedDocument(id);
            return RedirectToAction("UnsignedDocuments");
        }
        [HttpGet]
        public IActionResult Sign(int IdUnsignedDocument, string Signature)
        {
            KeysUnsignedDocumentViewModel keysUnsignedDocumentViewModel = new KeysUnsignedDocumentViewModel
            {
                UnsignedDocument = _UnsignedDocumentRepository.GetUnsignedDocumentById(IdUnsignedDocument),
                Key = _KeyRepository.GetKeyById(_UnsignedDocumentRepository.GetUnsignedDocumentById(IdUnsignedDocument).KeyId),
                Signature = Signature
            };
            return View(keysUnsignedDocumentViewModel);
        }
        [HttpPost]
        public IActionResult Sign(KeysUnsignedDocumentViewModel keysUnsignedDocumentViewModel)
        {
            KeysUnsignedDocumentViewModel keysUnsignedDocumentViewModelPass = new KeysUnsignedDocumentViewModel
            {
                UnsignedDocument = _UnsignedDocumentRepository.GetUnsignedDocumentById(keysUnsignedDocumentViewModel.IdUnsignedDocument),
                Key = _KeyRepository.GetKeyById(_UnsignedDocumentRepository.GetUnsignedDocumentById(keysUnsignedDocumentViewModel.IdUnsignedDocument).KeyId),
                Signature = keysUnsignedDocumentViewModel.Signature
            };
            if (!ModelState.IsValid)
            {
                return View(keysUnsignedDocumentViewModelPass);
            }

            string fileExtension = System.IO.Path.GetExtension(keysUnsignedDocumentViewModel.PkFile.FileName);
            if (fileExtension != ".pem")
            {
                ModelState.AddModelError("", "This is not a file generated by application!");
                return View(keysUnsignedDocumentViewModelPass);
            }

            UnsignedDocument unsignedDocument = _UnsignedDocumentRepository.GetUnsignedDocumentById(keysUnsignedDocumentViewModel.IdUnsignedDocument);
            var stream = new MemoryStream(unsignedDocument.Document);
            IFormFile documentFile = new FormFile(stream, 0, unsignedDocument.Document.Length, unsignedDocument.Name, unsignedDocument.Name);

            //Sign document
            SignModel signModel = _Sign.SignDocumentTest(keysUnsignedDocumentViewModel.PkFile, documentFile, unsignedDocument.KeyId, HttpContext);
            if (signModel.validKey == false || signModel.verifytest == false)
            {
                ModelState.AddModelError("", "Invalid key!");
                return View(keysUnsignedDocumentViewModelPass);
            }
            string signature = _Sign.SignDocument(signModel);


            //Store in database(for another user) SignedDocument
            SignedDocument signedDocument = new SignedDocument(unsignedDocument, signature, _HttpRequestFunctions.GetUser(HttpContext).Username);
            _SignedDocumentRepository.AddSignedDocument(signedDocument, _UserRepository.GetUserbyUsername(unsignedDocument.SentFromUsername));

            //store in db signed document for current user
            if (signedDocument.SentFromUsername != signedDocument.SignedByUsername)
            {
                SignedDocument signedDocumentCurrentUser = new SignedDocument(unsignedDocument, signature, _HttpRequestFunctions.GetUser(HttpContext).Username);
                _SignedDocumentRepository.AddSignedDocument(signedDocumentCurrentUser, _UserRepository.GetUserbyUsername(_HttpRequestFunctions.GetUser(HttpContext).Username));
            }

            //Add document in current user history
            _UnsignedDocumentRepository.MakeDocumentSigned(unsignedDocument);
            keysUnsignedDocumentViewModelPass.Signature = signature;

            return View(keysUnsignedDocumentViewModelPass);
        }


    }
}
