using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.ViewModels;
using Trustme.Service;
using Trustme.IServices;

namespace Trustme.Controllers
{
    public class LoadDocumentController : Controller
    {
        private IUserRepository _UserRepository;
        private IUnsignedDocumentRepository _UnsignedDocumentRepository;

        public LoadDocumentController(IUserRepository userRepository, IUnsignedDocumentRepository unsignedDocumentRepository)
        {
            _UserRepository = userRepository;
            _UnsignedDocumentRepository = unsignedDocumentRepository;
                
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadDocumentToSign()
        {
            return View();
        }

        public IActionResult LoadDocumentToSign(DocumentModel documentModel)
        {
            UserUnsignedDocument userUnsignedDocument = new UserUnsignedDocument();
            //unsignedDocument.Document = documentModel.Document;
            userUnsignedDocument.UnsignedDocument.KeyPreference = documentModel.CertificateName;
            if(_UserRepository.GetUserbyUsername(documentModel.Username) != null)
            {
                userUnsignedDocument.User = _UserRepository.GetUserbyUsername(documentModel.Username);

            }
            return View();
        }
    }
}
