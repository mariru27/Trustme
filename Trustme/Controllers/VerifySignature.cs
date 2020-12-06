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
    public class VerifySignature : Controller
    {
        private readonly AppContext _context;
        private IHostingEnvironment Environment;
        public Administration admin;

        public VerifySignature(AppContext context, IHostingEnvironment _environment, Administration _admin)
        {
            _context = context;
            Environment = _environment;
            admin = _admin;

        }

        public IActionResult VerifySign()
        {
            return View();
        }

        [HttpPost]
        public string VerifySignatureDocument(string username, string signature, IFormFile document)
        {

            if(ModelState.IsValid)
            {
                string wwwPath = this.Environment.WebRootPath;

                string publicKeystring = admin.getPublicKey(HttpContext);

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

                byte[] signaturebyte = Convert.FromBase64String(signature);

                if (sign.VerifySignature(signaturebyte))
                    return "is valid";
                else
                    return "not valid";


            }
            return "not valid";
        }
    }
}
