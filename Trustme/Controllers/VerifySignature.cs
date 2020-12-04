using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trustme.Controllers
{
    public class VerifySignature : Controller
    {
        public IActionResult Index()
        {
            return View();
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
                return "is valid";
            }
            return "not valid";
        }
    }
}
