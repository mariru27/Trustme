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
        private readonly IPendingRepository _PendingRepository;
        private readonly IEmailSender _EmailSender;


        public UploadController(IKeyRepository keyRepository, IEmailSender emailSender, IUserRepository userRepository, IUnsignedDocumentRepository unsignedDocumentRepository, IHttpRequestFunctions httpRequestFunctions, IPendingRepository pendingRepository)
        {
            _UserRepository = userRepository;
            _UnsignedDocumentRepository = unsignedDocumentRepository;
            _HttpRequestFunctions = httpRequestFunctions;
            _KeyRepository = keyRepository;
            _PendingRepository = pendingRepository;
            _EmailSender = emailSender;
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


                //get users
                User userUploadDocument = _HttpRequestFunctions.GetUser(HttpContext);
                User userReceiveDocument = _UserRepository.GetUserbyUsername(uploadDocumentModel.Username);

                //send pending
                _PendingRepository.AddPendingRequest(userReceiveDocument, userUploadDocument.Username);

                //if the same user upload and receive, mark accepted and don't send email notifications
                if (userUploadDocument.Username == userReceiveDocument.Username)
                {
                    _PendingRepository.MarkUserAcceptPendingFromUsername(userUploadDocument, userReceiveDocument.Username);
                }
                else
                {


                    //send email notification 
                    //------------------------------------------------------------------------------------------------------------
                    SendMailModel sendMailModel = new SendMailModel
                    {
                        ToUsername = userReceiveDocument.Username,
                        ToUserMail = userReceiveDocument.Mail,
                        MessageSubject = "New documents to sign",
                        MessageBodyHtml = "User " + unsignedDocument.SentFromUsername + "<a href=\"https://localhost:44318/SignDocuments/UnsignedDocuments\"> sent </a> you a document to sign!",
                    };


                    //send this email notification just if user was already accepted
                    if (_PendingRepository.CheckAcceptedPendingFromUsername(userReceiveDocument,
                                  userUploadDocument.Username) == false)
                    {
                        sendMailModel.MessageBodyHtml = "User " + unsignedDocument.SentFromUsername + " sent you a document to  , press <a href =\"https://localhost:44318/Pending/PendingList\"> allow </a> , to see documents!";
                    }

                    //send email just if user is not blocked
                    if (_PendingRepository.CheckBockedPendingFromUsername(userReceiveDocument,
                                  userUploadDocument.Username) == false)
                    {
                        _EmailSender.SendMail(sendMailModel);
                    }
                    //------------------------------------------------------------------------------------------------------------

                }

                TempData["SuccessUpload"] = "Uploaded Successfully!";
            }
            else
            {
                ModelState.AddModelError("", "User do not extist!");
            }

            return RedirectToAction("UploadDocument", new { Username = uploadDocument.Username });
        }
    }
}
