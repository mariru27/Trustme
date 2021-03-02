using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.ViewModels;
using Trustme.Service;
using Trustme.IServices;
using System.IO;
using Microsoft.AspNetCore.Http;


namespace Trustme.Controllers
{
    public class LoadDocumentController : Controller
    {
        private IUserRepository _UserRepository;
        private IUnsignedDocumentRepository _UnsignedDocumentRepository;
        private IHttpRequestFunctions _HttpRequestFunctions;
        private IKeyRepository _KeyRepository;

        public LoadDocumentController(IKeyRepository keyRepository, IUserRepository userRepository, IUnsignedDocumentRepository unsignedDocumentRepository, IHttpRequestFunctions httpRequestFunctions)
        {
            _UserRepository = userRepository;
            _UnsignedDocumentRepository = unsignedDocumentRepository;
            _HttpRequestFunctions = httpRequestFunctions;
            _KeyRepository = keyRepository;
        }

        [HttpGet]
        public IActionResult LoadDocumentToSign(string username)
        {
            UploadDocumentModel uploadDocumentModel = new UploadDocumentModel
            {
                Keys = _KeyRepository.ListAllKeys(_UserRepository.GetUserbyUsername(username)),
                Username = username
            };
            return View(uploadDocumentModel);
        }

        public IActionResult SendDocumentToUser()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult VerifyUser(string username)
        {
           User user = _UserRepository.GetUserbyUsername(username);
           if(user == null)
            {
                ModelState.AddModelError("", "Username do not exist");
                return RedirectToAction("SendDocumentToUser");
            }
           else
            {
                return RedirectToAction("LoadDocumentToSign",new { username = username });
            }
        }
        [HttpPost]
        public IActionResult LoadDocumentToSign(string Username, string CertificateName, IFormFile Document)
        {
            UserUnsignedDocument userUnsignedDocument = new UserUnsignedDocument();
            UnsignedDocument unsignedDocument = new UnsignedDocument
            {
                Name = Document.FileName
            };
            using (var target = new MemoryStream())
            {
                Document.CopyTo(target);
                unsignedDocument.Document = target.ToArray();
            }
            Key key = _KeyRepository.GetKeyByCertificateName(Username, CertificateName);

            unsignedDocument.KeyPreference = CertificateName;
            //unsignedDocument.Key = key;
            unsignedDocument.KeyId = key.KeyId;
            if (_UserRepository.GetUserbyUsername(Username) != null)
            {
                userUnsignedDocument.User = _UserRepository.GetUserbyUsername(Username);
                userUnsignedDocument.UnsignedDocument = unsignedDocument;
                //_UnsignedDocumentRepository.AddUnsignedDocument(userUnsignedDocument);

                UnsignedDocumentUserKey unsignedDocumentUserKey = new UnsignedDocumentUserKey
                {
                    Key = key,
                    UnsignedDocument = unsignedDocument,
                    User = _UserRepository.GetUserbyUsername(Username)
                };
                //unsignedDocumentUserKey.Key = key;

                _UnsignedDocumentRepository.AddUnsignedDocument(unsignedDocumentUserKey);
                TempData["Success"] = "Uploaded Successfully!";
            }
            else
            {
                ModelState.AddModelError("", "This user do not exist");
            }
            return RedirectToAction("LoadDocumentToSign", new { Username });
        }
    }
}
