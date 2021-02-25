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

            
            if (_HttpRequestFunctions.IsloggedIn(HttpContext) == true)
            {

                KeyPairCertificateGeneratorModel keyPairCertificateGeneratorModel = new KeyPairCertificateGeneratorModel();
                keyPairCertificateGeneratorModel = _Certificate.GenereateCertificate(keySize);

                _Certificate.CrateAndStoreKeyUserInDB(currentUser, keyPairCertificateGeneratorModel, key);
            }

            var result = _Certificate.CreateCertificateFileAndPrivateKeyFile(keyPairCertificateGeneratorModel, key.CertificateName, HttpContext);

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
