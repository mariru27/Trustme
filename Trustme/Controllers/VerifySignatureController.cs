﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IKeyRepository _KeyRepository;
        private readonly IUserRepository _UserRepository;
        private readonly ISign _Sign;
        public VerifySignatureController(IKeyRepository keyRepository, IUserRepository userRepository, ISign sign)
        {
            _UserRepository = userRepository;
            _KeyRepository = keyRepository;
            _Sign = sign;
        }

        [HttpGet]
        public IActionResult VerifySign(string Username)
        {
            //get users keys
            var keyList = _KeyRepository.ListAllKeys(_UserRepository.GetUserbyUsername(Username));
            //populate VerifySignModel with username and users keys
            VerifySignModel verifySignModel = new VerifySignModel
            {
                Username = Username,
                Keys = keyList
            };
            return View(verifySignModel);
        }

        [HttpGet]
        public IActionResult VerifyUser()
        {

            return View();
        }

        [HttpPost]
        public IActionResult VerifyUser(VerifyUserModel verifyUserModel)
        {
            //get current user and verify if exist
            if (!ModelState.IsValid)
            {
                return View();
            }
            User currentUser = _UserRepository.GetUserbyUsername(verifyUserModel.Username);
            if (currentUser == null)
            {
                ModelState.AddModelError("", "User do not exist!");
                return View();
            }
            else
            {
                //check if user that need to sign document have any certificates(keys)
                if (_KeyRepository.GetNrCertificates(_UserRepository.GetUserbyUsername(verifyUserModel.Username)) == 0)
                {
                    ModelState.AddModelError("", "User do not have any certificate! User need to generate a certificate!");
                    return View();
                }

                var keyList = _KeyRepository.ListAllKeys(currentUser);
                if (keyList != null)
                {

                    //pass to view keys and username
                    return RedirectToAction("VerifySign", new { Username = verifyUserModel.Username });
                }
            }
            return View();
        }


        [HttpPost]
        public IActionResult VerifySign(VerifySignatureDocumentModel verifySignatureDocumentModel)
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
            catch (Exception)
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
