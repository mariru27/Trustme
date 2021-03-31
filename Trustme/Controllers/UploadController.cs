using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Trustme.IServices;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    [Authorize(Roles = "Pro, Free")]
    public class UploadController : Controller
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUnsignedDocumentRepository _UnsignedDocumentRepository;
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        private readonly IKeyRepository _KeyRepository;

        public UploadController(IKeyRepository keyRepository, IUserRepository userRepository, IUnsignedDocumentRepository unsignedDocumentRepository, IHttpRequestFunctions httpRequestFunctions)
        {
            _UserRepository = userRepository;
            _UnsignedDocumentRepository = unsignedDocumentRepository;
            _HttpRequestFunctions = httpRequestFunctions;
            _KeyRepository = keyRepository;
        }

        [HttpGet]
        public IActionResult UploadDocument(string Username)
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
                return RedirectToAction("UploadDocument", new { Username = verifyUserModel.Username });
            }
        }
        [HttpPost]
        public IActionResult UploadDocument(UploadDocumentModel uploadDocumentModel)
        {
            if (!ModelState.IsValid)
            {
                return View(uploadDocumentModel.Username);
            }
            UserUnsignedDocument userUnsignedDocument = new UserUnsignedDocument();
            if (uploadDocumentModel.Document == null)
            {
                TempData["DocumentError"] = "You forgot to attach document!";
                return RedirectToAction("LoadDocumentToSign", new { uploadDocumentModel.Username });

            }
            UnsignedDocument unsignedDocument = new UnsignedDocument
            {
                Name = uploadDocumentModel.Document.FileName
            };
            using (var target = new MemoryStream())
            {
                uploadDocumentModel.Document.CopyTo(target);
                unsignedDocument.Document = target.ToArray();
            }
            Key key = _KeyRepository.GetKeyByCertificateName(uploadDocumentModel.Username, uploadDocumentModel.CertificateName);

            unsignedDocument.KeyPreference = uploadDocumentModel.CertificateName;
            //unsignedDocument.Key = key;
            unsignedDocument.KeyId = key.KeyId;
            unsignedDocument.SentFromUsername = _HttpRequestFunctions.GetUser(HttpContext).Username;
            if (_UserRepository.GetUserbyUsername(uploadDocumentModel.Username) != null)
            {
                userUnsignedDocument.User = _UserRepository.GetUserbyUsername(uploadDocumentModel.Username);
                userUnsignedDocument.UnsignedDocument = unsignedDocument;
                //_UnsignedDocumentRepository.AddUnsignedDocument(userUnsignedDocument);

                UnsignedDocumentUserKey unsignedDocumentUserKey = new UnsignedDocumentUserKey
                {
                    Key = key,
                    UnsignedDocument = unsignedDocument,
                    User = _UserRepository.GetUserbyUsername(uploadDocumentModel.Username)
                };
                //unsignedDocumentUserKey.Key = key;

                _UnsignedDocumentRepository.AddUnsignedDocument(unsignedDocumentUserKey);
                TempData["Success"] = "Uploaded Successfully!";
            }
            else
            {
                TempData["UserError"] = "User do not extist!";
            }
            return RedirectToAction("LoadDocumentToSign", new { uploadDocumentModel.Username });
        }
    }
}
