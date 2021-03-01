using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.ViewModels;
using Trustme.Service;
using Trustme.IServices;
using System.IO;
using Microsoft.AspNetCore.Http;


namespace Trustme.Controllers
{
    public class LoadDocumentController : Controller
    {
        private IUserRepository _UserRepository;
        private IUnsignedDocumentRepository _UnsignedDocumentRepository;
        private IHttpRequestFunctions _HttpRequestFunctions;

        public LoadDocumentController(IUserRepository userRepository, IUnsignedDocumentRepository unsignedDocumentRepository, IHttpRequestFunctions httpRequestFunctions)
        {
            _UserRepository = userRepository;
            _UnsignedDocumentRepository = unsignedDocumentRepository;
            _HttpRequestFunctions = httpRequestFunctions;
        }

        [HttpGet]
        public IActionResult LoadDocumentToSign()
        {
            return View();
        }

        public IActionResult SendDocumentToUser()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult VerifyUser(string username)
        {
           User user = _UserRepository.GetUserbyUsername(username);
           if(user == null)
            {
                return RedirectToAction("SendDocumentToUser");
            }
           else
            {
                return RedirectToAction("LoadDocumentToSign");
            }
        }
        [HttpPost]
        public IActionResult LoadDocumentToSign(string Username, string CertificateName, IFormFile Document)
        {

            UserUnsignedDocument userUnsignedDocument = new UserUnsignedDocument();
            UnsignedDocument unsignedDocument = new UnsignedDocument
            {
                Name = Document.FileName
            };
            using (var target = new MemoryStream())
            {
                Document.CopyTo(target);
                unsignedDocument.Document = target.ToArray();
            }
            unsignedDocument.KeyPreference = CertificateName;
            if (_UserRepository.GetUserbyUsername(Username) != null)
            {
                userUnsignedDocument.User = _UserRepository.GetUserbyUsername(Username);
                userUnsignedDocument.UnsignedDocument = unsignedDocument;
                _UnsignedDocumentRepository.AddUnsignedDocument(userUnsignedDocument);

            }
            else
            {
                ModelState.AddModelError("", "This user do not exist");
            }
            return View();
        }
    }
}
