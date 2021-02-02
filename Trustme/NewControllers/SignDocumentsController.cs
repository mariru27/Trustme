using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Trustme.Controllers
{
    //sign documents
    public class SignDocumentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
