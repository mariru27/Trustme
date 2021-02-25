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
using Trustme.IServices;
using Trustme.Service;
using Trustme.ViewModels;
using Trustme.Tools.ToolsModels;
using Trustme.ITools;
using Trustme.Tools;

namespace Trustme.Controllers
{
    public class GenerateCertificateController : Controller
    {
        private const string SignatureAlgorithm = "sha1WithRSA";
        private IHostingEnvironment Environment;
        private IHttpRequestFunctions _HttpRequestFunctions;
        private IKeyRepository _KeyRepository;
        private ICertificate _Certificate;
        private const int UserMaximNumberOfCertificates = 3;
        public GenerateCertificateController(ICertificate certificate, IHostingEnvironment _environment, IHttpRequestFunctions httpRequestFunctions, IKeyRepository keyRepository)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            Environment = _environment;
            _KeyRepository = keyRepository;
            _Certificate = certificate;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ErrorNrCertificates()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateCertificate(string certificateName, string description, int keySize)
        {
            User currentUser = _HttpRequestFunctions.GetUser(HttpContext);
            //create key
            Key key = new Key
            {
                CertificateName = certificateName,
                KeySize = keySize,
                Description = description
            };


            if(_KeyRepository.GetNrCertificates(currentUser) >= UserMaximNumberOfCertificates)
            {
                return RedirectToAction(nameof(ErrorNrCertificates));
            }

            TempData["certificateNameError"] = true;
            if (certificateName == null)
            {
                TempData["certificateNameError"] = false;
                return RedirectToAction("GenerateCertificate");
            }
            string wwwPath = this.Environment.WebRootPath;

            
            if (_HttpRequestFunctions.IsloggedIn(HttpContext) == true)
            {

                KeyPairCertificateGeneratorModel keyPairCertificateGeneratorModel = new KeyPairCertificateGeneratorModel();
                keyPairCertificateGeneratorModel = _Certificate.GenereateCertificate(keySize);

                _Certificate.CrateAndStoreKeyUserInDB(currentUser, keyPairCertificateGeneratorModel, key);
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

            result.FileDownloadName = certificateName + ".zip";


            return result;

        }



        public IActionResult GenerateCertificate()
        {
            if (TempData["certificateNameError"] != null && (bool)TempData["certificateNameError"] == false)
            {
                ModelState.AddModelError("", "Required certificate name");
            }
            return View();
        }

    }
}
