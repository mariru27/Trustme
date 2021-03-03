using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Trustme.Models;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;
using Org.BouncyCastle.OpenSsl;
using System.Text;
using System.Net.Http.Headers;
using AppContext = Trustme.Data.AppContext;
using Microsoft.EntityFrameworkCore;
using Trustme.IServices;
using Trustme.Service;
using Trustme.Models;
using Trustme.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trustme.Tools;
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

        public SignDocumentsController(IHostingEnvironment _environment, IKeyRepository keyRepository, IHttpRequestFunctions httpRequestFunctions, IUnsignedDocumentRepository unsignedDocumentRepository, ISign sign)
        {
            Environment = _environment;
            _KeyRepository = keyRepository;
            _HttpRequestFunctions = httpRequestFunctions;
            _Sign = sign;
            _UnsignedDocumentRepository = unsignedDocumentRepository;
        }

        public IActionResult UnsignedDocuments()
        {
            User user = new User();
            user = _HttpRequestFunctions.GetUser(HttpContext);
            IEnumerable<UnsignedDocument> unsignedDocuments = _UnsignedDocumentRepository.ListAllUsignedDocumentsByUser(user);
            return View(unsignedDocuments);
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
            SignedDocument signedDocument = new SignedDocument(unsignedDocument, signature);
            
            
            // _UnsignedDocumentRepository.MakeDocumentSigned(unsignedDocument);


            return RedirectToAction("SignSentDocument", new { IdUnsignedDocument = IdUnsignedDocument, Signature = signature });
        }
        public IActionResult SignDocument()
        {

            if (TempData["testKey"] != null && (bool)TempData["testKey"] == false)
            {
                ModelState.AddModelError("", "private key is not correct, you can generate another one if you lost it");
            }
            if (TempData["missingFiles"] != null && (bool)TempData["missingFiles"] == true)
            {
                ModelState.AddModelError("", "You are missing a file");
            }
            if (TempData["validKey"] != null && (bool)TempData["validKey"] == false)
            {
                ModelState.AddModelError("", "Private key is not valid");
            }
            User currentUser = _HttpRequestFunctions.GetUser(HttpContext);
            var certificates = _KeyRepository.ListAllKeys(currentUser);
            return View(certificates);
        }
        public async Task<IActionResult> SignDoc(IFormFile pkfile, IFormFile docfile, int certificates)
        {
            TempData["missingFiles"] = false;

            if (ModelState.IsValid && pkfile != null && docfile != null)
            {

                SignModel signModel = _Sign.SignDocumentTest(pkfile,docfile,certificates,HttpContext);
                TempData["validKey"] = true;

                if (signModel.validKey == false)
                {
                    TempData["validKey"] = false;
                    return RedirectToAction("SignDocument");
                }

                TempData["signature"] = "";
                TempData["testKey"] = true;

                if (signModel.verifytest == false)
                {
                    TempData["testKey"] = false;

                }
                else
                {
                    TempData["signature"] = _Sign.SignDocument(signModel);
                }
            }
            else
            {
                //is not valid
                TempData["missingFiles"] = true;
            }

            return RedirectToAction("SignDocument");
        }
    }
}
