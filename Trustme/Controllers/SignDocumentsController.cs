using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Controllers
{
    public class SignDocumentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
