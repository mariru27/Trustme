using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trustme.Models;
using Org.BouncyCastle.Crypto;
using Microsoft.AspNetCore.Hosting;
using Trustme.IServices;
using Trustme.ITools;
using Trustme.Tools.ToolsModels;

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

            //if (TempData["error"] != null && (bool)TempData["error"] == true)
            //{
            //    ModelState.AddModelError("", "You forgot to enter a username");
            //}
            //if (TempData["error2"] != null && (bool)TempData["error2"] == true)
            //{
            //    ModelState.AddModelError("", "User do not exist");
            //}
            return View();
        }

        public IActionResult VerifySign(string username)
        {
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
        public IActionResult VerifySignatureDocument(string username, string certificateName, string signature, IFormFile document)
        {
            TempData["SignatureError"] = false;
            TempData["documentError"] = false;

            if (ModelState.IsValid)
            {
                if (signature == null)
                {
                    TempData["SignatureError"] = true;
                    return RedirectToAction("VerifySign", new { username = username });
                }
                string wwwPath = this.Environment.WebRootPath;

                if (document == null)
                {
                    TempData["documentError"] = true;
                    return RedirectToAction("VerifySign", new { username = username });
                }

                VerifySignatureModel verifySignatureModel = new VerifySignatureModel
                {
                    CertificateName = certificateName,
                    Document = document,
                    Username = username
                };
                ISigner sign = _Sign.VerifySignature(verifySignatureModel);

                TempData["validSignature"] = "invalid";
                try
                {
                    Convert.FromBase64String(signature);

                }
                catch (Exception e)
                {
                    return RedirectToAction("VerifySign", new { username = username });
                };
                byte[] signaturebyte = Convert.FromBase64String(signature);


                if (sign.VerifySignature(signaturebyte))
                    TempData["validSignature"] = "valid";
                else
                    TempData["validSignature"] = "invalid";


            }
            TempData["validSignatrue"] = "invalid";
            return RedirectToAction("VerifySign", new { username = username });
        }


    }
}
