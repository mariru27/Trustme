using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Trustme.NewControllers
{
    //change phpto, edit all values 
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
