using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Trustme.IServices;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    [Authorize(Roles = "Pro, User, Free")]
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
            //check if user exist 
            User user = _UserRepository.GetUserbyUsername(username);
            if (user == null)
            {
                TempData["UserError"] = "User do not extist!";
                return RedirectToAction("SendDocumentToUser");
            }
            else
            {
                //check if user that need to sign document have any certificates(keys)
                if (_KeyRepository.GetNrCertificates(user) == 0)
                {
                    TempData["UserDontHaveCertificates"] = "User do not have any certificate! User need to generate a certificate!";
                    return RedirectToAction("SendDocumentToUser");
                }
                return RedirectToAction("LoadDocumentToSign", new { username = username });
            }
        }
        [HttpPost]
        public IActionResult LoadDocumentToSign(string Username, string CertificateName, IFormFile Document)
        {
            UserUnsignedDocument userUnsignedDocument = new UserUnsignedDocument();
            if (Document == null)
            {
                TempData["DocumentError"] = "You forgot to attach document!";
                return RedirectToAction("LoadDocumentToSign", new { Username });

            }
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
            unsignedDocument.SentFromUsername = _HttpRequestFunctions.GetUser(HttpContext).Username;
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
                TempData["UserError"] = "User do not extist!";
            }
            return RedirectToAction("LoadDocumentToSign", new { Username });
        }
    }
}
