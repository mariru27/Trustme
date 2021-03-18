using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Controllers
{
    /// <summary>
    /// Admin user type can delete, 
    /// edit user info
    /// </summary>
    public class AdministratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UsersList()
        {
            return View();
        }
    }
}
