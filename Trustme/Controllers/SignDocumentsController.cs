﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trustme.Models;
using Microsoft.AspNetCore.Hosting;
using Trustme.IServices;
using Trustme.ViewModels;
using Trustme.ITools;
using Trustme.Tools.ToolsModels;

namespace Trustme.Controllers
{
    [Authorize]

    public class SignDocumentsController : Controller
    {
        private const string SignatureAlgorithm = "sha1WithRSA";
        private IHostingEnvironment Environment;
        private IKeyRepository _KeyRepository;
        private IHttpRequestFunctions _HttpRequestFunctions;
        private IUnsignedDocumentRepository _UnsignedDocumentRepository;
        private ISign _Sign;
        private IUserRepository _UserRepository;
        private ISignedDocumentRepository _SignedDocumentRepository;
        public SignDocumentsController(IHostingEnvironment _environment, IUserRepository userRepository,ISignedDocumentRepository signedDocumentRepository, IKeyRepository keyRepository, IHttpRequestFunctions httpRequestFunctions, IUnsignedDocumentRepository unsignedDocumentRepository, ISign sign)
        {
            Environment = _environment;
            _KeyRepository = keyRepository;
            _HttpRequestFunctions = httpRequestFunctions;
            _Sign = sign;
            _UnsignedDocumentRepository = unsignedDocumentRepository;
            _SignedDocumentRepository = signedDocumentRepository;
            _UserRepository = userRepository;
        }

        public IActionResult UnsignedDocuments()
        {
            User user = new User();
            user = _HttpRequestFunctions.GetUser(HttpContext);
            IEnumerable<UnsignedDocument> unsignedDocuments = _UnsignedDocumentRepository.ListAllUsignedDocumentsByUser(user);
            return View(unsignedDocuments);
        }

        [HttpGet]

        public IActionResult DeleteDocument(int id)
        {
            _UnsignedDocumentRepository.DeleteUnsignedDocument(id);
            return RedirectToAction("UnsignedDocuments");
        }


        public IActionResult SignSentDocument(int IdUnsignedDocument, string Signature)
        {
            KeysUnsignedDocumentViewModel keysUnsignedDocumentViewModel = new KeysUnsignedDocumentViewModel
            {
                UnsignedDocument = _UnsignedDocumentRepository.GetUnsignedDocumentById(IdUnsignedDocument),
                Key = _KeyRepository.GetKeyById(_UnsignedDocumentRepository.GetUnsignedDocumentById(IdUnsignedDocument).KeyId),
                Signature = Signature
            };
            return View(keysUnsignedDocumentViewModel);
        }
        
        public IActionResult SignSentDocumentCard(int IdUnsignedDocument, IFormFile PkFile)
        {
            if(PkFile == null)
            {
                TempData["PKNull"] = "You forgot to attach private key file!";
                return RedirectToAction("SignSentDocument", new { IdUnsignedDocument = IdUnsignedDocument });
            }

            UnsignedDocument unsignedDocument = _UnsignedDocumentRepository.GetUnsignedDocumentById(IdUnsignedDocument);
            var stream = new MemoryStream(unsignedDocument.Document);
            IFormFile documentFile = new FormFile(stream, 0, unsignedDocument.Document.Length, unsignedDocument.Name, unsignedDocument.Name);
            
            //Sign document
            SignModel signModel = _Sign.SignDocumentTest(PkFile, documentFile, unsignedDocument.KeyId, HttpContext);
            if(signModel.validKey == false || signModel.verifytest == false)
            {
                TempData["InvalidKey"] = "Invalid key!";
                return RedirectToAction("SignSentDocument", new { IdUnsignedDocument = IdUnsignedDocument });
            }
            string signature = _Sign.SignDocument(signModel);

            
            //Store in database SignedDocument
            SignedDocument signedDocument = new SignedDocument(unsignedDocument, signature, _HttpRequestFunctions.GetUser(HttpContext).Username);
            _SignedDocumentRepository.AddSignedDocument(signedDocument, _UserRepository.GetUserbyUsername(unsignedDocument.SentFromUsername));
            _UnsignedDocumentRepository.MakeDocumentSigned(unsignedDocument);


            return RedirectToAction("SignSentDocument", new { IdUnsignedDocument = IdUnsignedDocument, Signature = signature });
        }
        
       
    }
}
