using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trustme.Models;
using Org.BouncyCastle.Crypto;
using Microsoft.AspNetCore.Hosting;
using Trustme.IServices;
using Trustme.ITools;
using Trustme.Tools.ToolsModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        //public IActionResult VerifySign(string username)
        //{
        //    ViewData["username"] = username;
        //    TempData["error"] = false;
        //    TempData["error2"] = false;

        //    if (TempData["SignatureError"] != null && (bool)TempData["SignatureError"] == true)
        //    {
        //        ModelState.AddModelError("", "Require signature");

        //        User currentUser = _UserRepository.GetUserbyUsername(username);
        //        var keyList = _KeyRepository.ListAllKeys(currentUser);
        //        if (keyList != null)
        //            return View(keyList);
        //    }
        //    if (TempData["documentError"] != null && (bool)TempData["documentError"] == true)
        //    {
        //        TempData["documentError"] = false;
        //        ModelState.AddModelError("", "Required file");

        //        User currentUser = _UserRepository.GetUserbyUsername(username);
        //        var keyList = _KeyRepository.ListAllKeys(currentUser);
        //        if (keyList != null)
        //            return View(keyList);
        //    }
        //    if (username != null)
        //    {
        //        User currentUser = _UserRepository.GetUserbyUsername(username);
        //        var keyList = _KeyRepository.ListAllKeys(currentUser);
        //        if (keyList != null)
        //            return View(keyList);
        //        else
        //        {
        //            TempData["error2"] = true;
        //            return RedirectToAction("VerifyUser");
        //        }
        //    }
        //    else
        //        TempData["error"] = true;
        //    return RedirectToAction("VerifyUser");
        //}


        [HttpPost]
        public IActionResult VerifySignatureDocument(VerifySignatureDocumentModel verifySignatureDocumentModel)
        {

            //validate signature
            if(verifySignatureDocumentModel.Signature == null)
            {
                TempData["Error_MissingSignature"] = "Signature is missing!";
                return RedirectToAction("VerifySign", new { Username = verifySignatureDocumentModel.Username });

            }
            //validate document
            if(verifySignatureDocumentModel.Document == null)
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
                return RedirectToAction("VerifySign", new { username = verifySignatureDocumentModel.Username });
            };
            byte[] signaturebyte = Convert.FromBase64String(verifySignatureDocumentModel.Signature);

            if (sign.VerifySignature(signaturebyte) == false)
                TempData["Error_InvalidSignature"] = "Invalid signature!";
            else
                TempData["ValidSignature"] = "Signature is valid!";
           
            return RedirectToAction("VerifySign", new { username = verifySignatureDocumentModel.Username});
        }


    }
}
