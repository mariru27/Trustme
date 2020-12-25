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
    public class SignDocuments : Controller
    {
        public Administration admin;
        private const string SignatureAlgorithm = "sha1WithRSA";
        private IHostingEnvironment Environment;
        private readonly AppContext _context;


        public SignDocuments(Administration _admin, IHostingEnvironment _environment, AppContext context)
        {
            admin = _admin;
            Environment = _environment;
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        public string test()
        {
            if (admin.isloggedIn(HttpContext) == false)
                return "nu este logat";
            return "este logat";
        }


        public async Task<IActionResult> Signdocument()
        { 

            if (TempData["testKey"] != null && (bool)TempData["testKey"] == false)
            {
                ModelState.AddModelError("", "private key is not correct, you can generate another one if you lost it");
            }
            if(TempData["missingFiles"] != null && (bool)TempData["missingFiles"] == true)
            {
                ModelState.AddModelError("", "You are missing a file");
            }

            return View(await _context.Key.Where(a => a.UserId == admin.getUserId(HttpContext)).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateCertificate(string certificateName, string description)
        {
            string wwwPath = this.Environment.WebRootPath;

            // Keypair Generator
            RsaKeyPairGenerator kpGenerator = new RsaKeyPairGenerator();
            kpGenerator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));

            // Create a keypair
            AsymmetricCipherKeyPair kp = kpGenerator.GenerateKeyPair();

            // Certificate Generator
            X509V3CertificateGenerator cGenerator = new X509V3CertificateGenerator();
            cGenerator.SetSerialNumber(BigInteger.ProbablePrime(120, new Random()));
            cGenerator.SetSubjectDN(new X509Name("CN=" + "trustme.com"));
            cGenerator.SetIssuerDN(new X509Name("CN=" + "Trustme Application"));
            cGenerator.SetNotBefore(DateTime.Now);
            cGenerator.SetNotAfter(DateTime.Now.Add(new TimeSpan(365, 0, 0, 0))); // Expire in 1 year
            cGenerator.SetSignatureAlgorithm(SignatureAlgorithm); // See the Appendix Below for info on the hash types supported by Bouncy Castle C#
            cGenerator.SetPublicKey(kp.Public); // Only the public key should be used here!
            //we saved public key in database
            if (admin.isloggedIn(HttpContext) == true)
            {
                
                string username = admin.getUsername(HttpContext);
                

                TextWriter textWriter1 = new StringWriter();
                PemWriter pemWriter1 = new PemWriter(textWriter1);
                pemWriter1.WriteObject(kp.Public);
                pemWriter1.Writer.Flush();

                string publicKey = textWriter1.ToString();
                admin.addPublicKey(username, publicKey,certificateName,description);
            }

            var cert = cGenerator.Generate(kp.Private); // Create a self-signed cert

            byte[] encoded = cert.GetEncoded();
            
            string pathDir = Path.Combine(wwwPath, "Certificate_PKey");
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }


            string pathCertificate = Path.Combine(pathDir, "certificate.der");
            using (FileStream outStream = new FileStream(pathCertificate, FileMode.Create, FileAccess.ReadWrite))
            {
                outStream.Write(encoded, 0, encoded.Length);
            }

            PrivateKeyInfo pkInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(kp.Private);
            string privatekey = Convert.ToBase64String(pkInfo.GetDerEncoded());


            TextWriter textWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(kp.Private);
            pemWriter.Writer.Flush();

            string privateKey = textWriter.ToString();

            byte[] privatekey_byte = Encoding.ASCII.GetBytes(privateKey);

            string pathPrivateKey = Path.Combine(pathDir, "privateKey.pem");
            using (FileStream outStream = new FileStream(pathPrivateKey, FileMode.Create, FileAccess.ReadWrite))
            {
                outStream.Write(privatekey_byte, 0, privatekey_byte.Length);
            }
            
            string pathDirectoryZip = Path.Combine(wwwPath, "Certificate_Key");

            ZipFile.CreateFromDirectory(pathDir, pathDirectoryZip, System.IO.Compression.CompressionLevel.Optimal, false);

            const string contentType = "application/zip";
            HttpContext.Response.ContentType = contentType;
            var result = new FileContentResult(System.IO.File.ReadAllBytes(pathDirectoryZip), contentType);


            System.IO.DirectoryInfo dir = new DirectoryInfo(pathDir);
            foreach (FileInfo files in dir.GetFiles())
            {
                files.Delete();
            }
            Directory.Delete(pathDir);
            System.IO.File.Delete(pathDirectoryZip);

           
            return result;

        }

        public Org.BouncyCastle.Crypto.AsymmetricKeyParameter ReadAsymmetricKeyParameter(string pemFilename)
        {
            var fileStream = System.IO.File.OpenText(pemFilename);
            var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(fileStream);
            var KeyParameter = (Org.BouncyCastle.Crypto.AsymmetricKeyParameter)pemReader.ReadObject();
            return KeyParameter;
        }

        public async Task<IActionResult> SignDoc(IFormFile pkfile, IFormFile docfile)
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
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)o;
                AsymmetricKeyParameter privatekeyy = keyPair.Private;

                //just for test
                //-------begin--test------------------------------------------------------
                string testmessage = "this is a test message";
                byte[] testmessagetyte = Encoding.ASCII.GetBytes(testmessage);

                //phrase public key
                string publicKeystring = admin.getPublicKey(HttpContext);

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