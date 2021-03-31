using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Trustme.IServices;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    [Authorize(Roles = "Pro, Free")]
    public class UploadDocumentController : Controller
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUnsignedDocumentRepository _UnsignedDocumentRepository;
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        private readonly IKeyRepository _KeyRepository;

        public UploadDocumentController(IKeyRepository keyRepository, IUserRepository userRepository, IUnsignedDocumentRepository unsignedDocumentRepository, IHttpRequestFunctions httpRequestFunctions)
        {
            _UserRepository = userRepository;
            _UnsignedDocumentRepository = unsignedDocumentRepository;
            _HttpRequestFunctions = httpRequestFunctions;
            _KeyRepository = keyRepository;
        }

        [HttpGet]
        public IActionResult LoadDocumentToSign(string Username)
        {
            UploadDocumentModel uploadDocumentModel = new UploadDocumentModel
            {
                Keys = _KeyRepository.ListAllKeys(_UserRepository.GetUserbyUsername(Username)),
                Username = Username
            };
            return View(uploadDocumentModel);
        }

        [HttpGet]
        public IActionResult UploadUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadUser(VerifyUserModel verifyUserModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //check if user exist 
            User user = _UserRepository.GetUserbyUsername(verifyUserModel.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "User do not exist!");
                return View();
            }
            else
            {
                //check if user that need to sign document have any certificates(keys)
                if (_KeyRepository.GetNrCertificates(user) == 0)
                {
                    ModelState.AddModelError("", "User do not have any certificate! User need to generate a certificate!");
                    return View();
                }
                return RedirectToAction("LoadDocumentToSign", new { Username = verifyUserModel.Username });
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
