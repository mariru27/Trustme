﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
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
        public IActionResult UnsignedDocuments()
        {

            IEnumerable<UnsignedDocument> unsignedDocuments = _UnsignedDocumentRepository.ListAllUsignedDocumentsByUser(_HttpRequestFunctions.GetUser(HttpContext));
            return View(unsignedDocuments);
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

            if (keysUnsignedDocumentViewModel.PkFile == null)
            {
                TempData["PKNull"] = "You forgot to attach private key file!";
                return RedirectToAction("SignSentDocument", new { IdUnsignedDocument = keysUnsignedDocumentViewModel.IdUnsignedDocument });
            }
            string fileExtension = System.IO.Path.GetExtension(keysUnsignedDocumentViewModel.PkFile.FileName);
            if (fileExtension != ".pem")
            {
                TempData["FileError"] = "This is not a file generated by application!";
                return RedirectToAction("SignSentDocument", new { IdUnsignedDocument = keysUnsignedDocumentViewModel.IdUnsignedDocument });

            }

            UnsignedDocument unsignedDocument = _UnsignedDocumentRepository.GetUnsignedDocumentById(keysUnsignedDocumentViewModel.IdUnsignedDocument);
            var stream = new MemoryStream(unsignedDocument.Document);
            IFormFile documentFile = new FormFile(stream, 0, unsignedDocument.Document.Length, unsignedDocument.Name, unsignedDocument.Name);

            //Sign document
            SignModel signModel = _Sign.SignDocumentTest(keysUnsignedDocumentViewModel.PkFile, documentFile, unsignedDocument.KeyId, HttpContext);
            if (signModel.validKey == false || signModel.verifytest == false)
            {
                TempData["InvalidKey"] = "Invalid key!";
                return RedirectToAction("SignSentDocument", new { IdUnsignedDocument = keysUnsignedDocumentViewModel.IdUnsignedDocument });
            }
            string signature = _Sign.SignDocument(signModel);


            //Store in database(for another user) SignedDocument
            SignedDocument signedDocument = new SignedDocument(unsignedDocument, signature, _HttpRequestFunctions.GetUser(HttpContext).Username);
            _SignedDocumentRepository.AddSignedDocument(signedDocument, _UserRepository.GetUserbyUsername(unsignedDocument.SentFromUsername));

            //store in db signed document for current user
            SignedDocument signedDocumentCurrentUser = new SignedDocument(unsignedDocument, signature, _HttpRequestFunctions.GetUser(HttpContext).Username);
            _SignedDocumentRepository.AddSignedDocument(signedDocumentCurrentUser, _UserRepository.GetUserbyUsername(_HttpRequestFunctions.GetUser(HttpContext).Username));

            //Add document in current user history
            _UnsignedDocumentRepository.MakeDocumentSigned(unsignedDocument);


            return RedirectToAction("SignSentDocument", new { IdUnsignedDocument = keysUnsignedDocumentViewModel.IdUnsignedDocument, Signature = signature });
        }


    }
}
