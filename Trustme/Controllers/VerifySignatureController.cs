using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using System;
using Trustme.IServices;
using Trustme.ITools;
using Trustme.Models;
using Trustme.Tools.ToolsModels;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    public class VerifySignatureController : Controller
    {
        private IHostingEnvironment Environment;
        private IKeyRepository _KeyRepository;
        private IUserRepository _UserRepository;
        private ISign _Sign;
        public VerifySignatureController(IHostingEnvironment _environment, IKeyRepository keyRepository, IUserRepository userRepository, ISign sign)
        {
            _UserRepository = userRepository;
            _KeyRepository = keyRepository;
            Environment = _environment;
            _Sign = sign;
        }
        public IActionResult VerifyUser()
        {

            return View();
        }

        public IActionResult VerifySign(string Username)
        {

            if (Username == null)
            {
                TempData["Error_UsernameMissing"] = "Username is missing!";
                return RedirectToAction("VerifyUser");

            }

            //get current user and verify if exist
            User currentUser = _UserRepository.GetUserbyUsername(Username);
            if (currentUser == null)
            {
                TempData["Error_UserDoNotExist"] = "User do not exist!";
                return RedirectToAction("VerifyUser");

            }
            else
            {
                //check if user that need to sign document have any certificates(keys)
                if (_KeyRepository.GetNrCertificates(_UserRepository.GetUserbyUsername(Username)) == 0)
                {
                    TempData["UserDontHaveCertificates"] = "User do not have any certificate! User need to generate a certificate!";
                    return RedirectToAction("VerifyUser");
                }


                var keyList = _KeyRepository.ListAllKeys(currentUser);
                if (keyList != null)
                {
                    VerifySignModel verifySignModel = new VerifySignModel
                    {
                        Username = Username,
                        Keys = keyList
                    };
                    //pass to view keys and username
                    return View(verifySignModel);

                }
            }
            return RedirectToAction("VerifyUser");



        }


        [HttpPost]
        public IActionResult VerifySignatureDocument(VerifySignatureDocumentModel verifySignatureDocumentModel)
        {

            //validate signature
            if (verifySignatureDocumentModel.Signature == null)
            {
                TempData["Error_MissingSignature"] = "Signature is missing!";
                return RedirectToAction("VerifySign", new { Username = verifySignatureDocumentModel.Username });

            }
            //validate document
            if (verifySignatureDocumentModel.Document == null)
            {
                TempData["Error_MissingDocument"] = "Document is missing!";
                return RedirectToAction("VerifySign", new { Username = verifySignatureDocumentModel.Username });
            }


            VerifySignatureModel verifySignatureModel = new VerifySignatureModel
            {
                CertificateName = verifySignatureDocumentModel.CertificateName,
                Document = verifySignatureDocumentModel.Document,
                Username = verifySignatureDocumentModel.Username
            };

            //verify signature
            ISigner sign = _Sign.VerifySignature(verifySignatureModel);

            try
            {
                Convert.FromBase64String(verifySignatureDocumentModel.Signature);

            }
            catch (Exception e)
            {
                TempData["Error_CorruptedSignature"] = "Signature was corrupted!";
                return RedirectToAction("VerifySign", new { username = verifySignatureDocumentModel.Username });
            };
            byte[] signaturebyte = Convert.FromBase64String(verifySignatureDocumentModel.Signature);

            if (sign.VerifySignature(signaturebyte) == false)
                TempData["Error_InvalidSignature"] = "Invalid signature!";
            else
                TempData["ValidSignature"] = "Signature is valid!";

            return RedirectToAction("VerifySign", new { username = verifySignatureDocumentModel.Username });
        }


    }
}
