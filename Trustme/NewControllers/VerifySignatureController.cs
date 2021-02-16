using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Trustme.Controllers
{
    //display all user keys, verify signature
    public class VerifySignature1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
