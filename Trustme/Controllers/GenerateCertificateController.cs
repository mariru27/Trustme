using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Trustme.Controllers
{
    public class GenerateCertificateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
