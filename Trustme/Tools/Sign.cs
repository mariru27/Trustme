using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trustme.ITools;
using Trustme.IServices;
using Trustme.Service;

namespace Trustme.Tools
{
    public class Sign : ISign
    {
        private IHostingEnvironment Environment;
        private IKeyRepository _KeyRepository;
        private IHttpRequestFunctions _HttpRequestFunctions;
        
        public Sign(IHostingEnvironment hostingEnvironment, KeyRepository keyRepository, IHttpRequestFunctions httpRequestFunctions)
        {
            Environment = hostingEnvironment;
            _KeyRepository = keyRepository;
            _HttpRequestFunctions = httpRequestFunctions;
        }
        public bool SignDoc(IFormFile pkfile, IFormFile docfile, int certificates, HttpContext httpContext)
        {

            var wwwfilePath = this.Environment.WebRootPath; //we are using Temp file name just for the example. Add your own file path.c
            wwwfilePath = Path.Combine(wwwfilePath, "dirForPK");
            var filePath = Path.Combine(wwwfilePath, pkfile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                pkfile.CopyTo(stream);
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
            //TempData["validKey"] = false;
            //if (o == null)
            //{
            //    TempData["validKey"] = true;
            //    return RedirectToAction("SignDocument");
            //}
            AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)o;
            AsymmetricKeyParameter privatekeyy = keyPair.Private;

            //just for test
            //-------begin--test------------------------------------------------------
            string testmessage = "this is a test message";
            byte[] testmessagetyte = Encoding.ASCII.GetBytes(testmessage);

            var currentKey = _KeyRepository.GetKey(_HttpRequestFunctions.GetUserId(httpContext), certificates);

            byte[] publickeybyte = Encoding.ASCII.GetBytes(currentKey.PublicKey);
            //phrase public key
            var readerPublickey = new StringReader(currentKey.PublicKey);
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

            return verifytest;
        }
    }
}
