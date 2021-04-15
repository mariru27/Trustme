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
            UploadDocumentModel uploadDocument = new UploadDocumentModel
            {
                Keys = _KeyRepository.ListAllKeys(_UserRepository.GetUserbyUsername(uploadDocumentModel.Username)),
                Username = uploadDocumentModel.Username
            };
            if (!ModelState.IsValid)
            {
                return View(uploadDocument);
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
            unsignedDocument.KeyId = key.KeyId;
            unsignedDocument.SentFromUsername = _HttpRequestFunctions.GetUser(HttpContext).Username;
            unsignedDocument.ContentType = uploadDocumentModel.Document.ContentType;

            UserUnsignedDocument userUnsignedDocument = new UserUnsignedDocument();
            if (_UserRepository.GetUserbyUsername(uploadDocumentModel.Username) != null)
            {
                userUnsignedDocument.User = _UserRepository.GetUserbyUsername(uploadDocumentModel.Username);
                userUnsignedDocument.UnsignedDocument = unsignedDocument;
                UnsignedDocumentUserKey unsignedDocumentUserKey = new UnsignedDocumentUserKey
                {
                    Key = key,
                    UnsignedDocument = unsignedDocument,
                    User = _UserRepository.GetUserbyUsername(uploadDocumentModel.Username)
                };
                _UnsignedDocumentRepository.AddUnsignedDocument(unsignedDocumentUserKey);
                TempData["SuccessUpload"] = "Uploaded Successfully!";
            }
            else
            {
                ModelState.AddModelError("", "User do not extist!");
            }
            return View(uploadDocument);
        }
    }
}
