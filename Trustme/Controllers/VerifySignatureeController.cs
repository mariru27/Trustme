using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppContext = Trustme.Data.AppContext;
using Trustme.Models;
using Org.BouncyCastle.OpenSsl;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace Trustme.Controllers
{
    public class VerifySignatureeController : Controller
    {
        private readonly AppContext _context;
        private IHostingEnvironment Environment;
        public Administration admin;

        public VerifySignatureeController(AppContext context, IHostingEnvironment _environment, Administration _admin)
        {
            _context = context;
            Environment = _environment;
            admin = _admin;
        }
        public IActionResult VerifyUser()
        {

            if (TempData["error"] != null && (bool)TempData["error"] == true)
            {
                ModelState.AddModelError("", "You forgot to enter a username");
            }
            if (TempData["error2"] != null && (bool)TempData["error2"] == true)
            {
                ModelState.AddModelError("", "User do not exist");
            }
            return View();
        }
        public IActionResult VerifySign(string username)
        {
            ViewData["username"] = username;
            TempData["error"] = false;
            TempData["error2"] = false;

            if (TempData["SignatureError"] != null && (bool)TempData["SignatureError"] == true)
            {
                ModelState.AddModelError("", "Require signature");
                var keyList = admin.getAllKeysByUsername(username);
                if (keyList != null)
                    return View(keyList);
            }
            if (TempData["documentError"] != null && (bool)TempData["documentError"] == true)
            {
                TempData["documentError"] = false;
                ModelState.AddModelError("", "Required file");
                var keyList = admin.getAllKeysByUsername(username);
                if (keyList != null)
                    return View(keyList);
            }
            if (username != null)
            {
                var keyList = admin.getAllKeysByUsername(username);
                if (keyList != null)
                    return View(keyList);
                else
                {
                    TempData["error2"] = true;
                    return RedirectToAction("VerifyUser");
                }
            }
            else
                TempData["error"] = true;
            return RedirectToAction("VerifyUser");
        }


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

                //get public key by name from database, use key to decrypt

                string publicKeystring = admin.getPublicKeyByCertificateName(username, certificateName);

                byte[] publickeybyte = Encoding.ASCII.GetBytes(publicKeystring);

                var reader = new StringReader(publicKeystring);
                var keypem = new PemReader(reader);

                var publickey = (Org.BouncyCastle.Crypto.AsymmetricKeyParameter)keypem.ReadObject();

                reader.Close();

                byte[] fileBytesdoc;

                using (var ms = new MemoryStream())
                {
                    document.CopyTo(ms);
                    fileBytesdoc = ms.ToArray();
                }

                ISigner sign = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id);
                sign.Init(false, publickey);
                sign.BlockUpdate(fileBytesdoc, 0, fileBytesdoc.Length);

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
