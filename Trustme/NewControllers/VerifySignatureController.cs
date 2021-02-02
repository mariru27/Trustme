using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Trustme.Controllers
{
    //display all user keys, verify signature
    public class VerifySignatureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
