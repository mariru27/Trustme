using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Trustme.Models;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;
using Org.BouncyCastle.OpenSsl;
using System.Text;
using System.Net.Http.Headers;
using AppContext = Trustme.Data.AppContext;
using Microsoft.EntityFrameworkCore;

namespace Trustme.Controllers
{
    [Authorize]

    public class SignDocuments1Controller : Controller
    {
        public Administration admin;
        private const string SignatureAlgorithm = "sha1WithRSA";
        private IHostingEnvironment Environment;
        private readonly AppContext _context;

        public SignDocuments1Controller(Administration _admin, IHostingEnvironment _environment, AppContext context)
        {
            admin = _admin;
            Environment = _environment;
            _context = context;

        }

        public IActionResult Signdocument()
        {

            if (TempData["testKey"] != null && (bool)TempData["testKey"] == false)
            {
                ModelState.AddModelError("", "private key is not correct, you can generate another one if you lost it");
            }
            if (TempData["missingFiles"] != null && (bool)TempData["missingFiles"] == true)
            {
                ModelState.AddModelError("", "You are missing a file");
            }
            if (TempData["validKey"] != null && (bool)TempData["validKey"] == true)
            {
                ModelState.AddModelError("", "Private key is not valid");
            }
            var certificates = admin.getAllKeys(HttpContext);
            return View(certificates);
        }
        public async Task<IActionResult> SignDoc(IFormFile pkfile, IFormFile docfile, int certificates)
        {
            TempData["missingFiles"] = false;

            if (ModelState.IsValid && pkfile != null && docfile != null)
            {


                var wwwfilePath = this.Environment.WebRootPath; //we are using Temp file name just for the example. Add your own file path.c
                wwwfilePath = Path.Combine(wwwfilePath, "dirForPK");
                var filePath = Path.Combine(wwwfilePath, pkfile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    await pkfile.CopyToAsync(stream);
                }

                byte[] fileBytesdoc;
                using (var ms = new MemoryStream())
                {
                    docfile.CopyTo(ms);
                    fileBytesdoc = ms.ToArray();
                }
                //read private key and phrase
                string keypath = Path.Combine(wwwfilePath, pkfile.FileName);
                var reader = System.IO.File.OpenText(keypath);
                var keypem = new PemReader(reader);
                var o = keypem.ReadObject();
                TempData["validKey"] = false;
                if (o == null)
                {
                    TempData["validKey"] = true;
                    return RedirectToAction("SignDocument");
                }
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)o;
                AsymmetricKeyParameter privatekeyy = keyPair.Private;

                //just for test
                //-------begin--test------------------------------------------------------
                string testmessage = "this is a test message";
                byte[] testmessagetyte = Encoding.ASCII.GetBytes(testmessage);

                //phrase public key
                string publicKeystring = admin.getPublicKey(HttpContext, certificates);

                byte[] publickeybyte = Encoding.ASCII.GetBytes(publicKeystring);

                var readerPublickey = new StringReader(publicKeystring);
                var pemPublicKey = new PemReader(readerPublickey);

                var publickey = (Org.BouncyCastle.Crypto.AsymmetricKeyParameter)pemPublicKey.ReadObject();

                reader.Close();

                ISigner signtest = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id);
                signtest.Init(true, privatekeyy);
                signtest.BlockUpdate(testmessagetyte, 0, testmessagetyte.Length);
                var signaturetest = signtest.GenerateSignature();
                string signatureteststring = Convert.ToBase64String(signaturetest);

                signtest.Init(false, publickey);
                signtest.BlockUpdate(testmessagetyte, 0, testmessagetyte.Length);

                byte[] signaturetestbyte = Convert.FromBase64String(signatureteststring);

                var verifytest = signtest.VerifySignature(signaturetestbyte);
                //------end--test-----------------------------------------------------------------

                TempData["signature"] = "";
                TempData["testKey"] = true;
                if (verifytest == false)
                {
                    TempData["testKey"] = false;

                }
                else
                {
                    ISigner sign = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id);
                    sign.Init(true, privatekeyy);
                    sign.BlockUpdate(fileBytesdoc, 0, fileBytesdoc.Length);
                    var signature = sign.GenerateSignature();
                    string signaturestring = Convert.ToBase64String(signature);

                    reader.Close();
                    System.IO.File.Delete(keypath);
                    TempData["signature"] = signaturestring;

                }
            }
            else
            {
                //is not valid
                TempData["missingFiles"] = true;
            }

            return RedirectToAction("SignDocument");
        }
    }
}
