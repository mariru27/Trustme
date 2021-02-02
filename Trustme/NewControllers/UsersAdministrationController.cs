using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Trustme.Controllers
{
    //delete, edit user profile, change user profile, 
    //change user key name, 
    //block user
    public class UsersAdministrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
