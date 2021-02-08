using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Trustme.NewControllers
{
    //change phpto, edit all values, display
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Profile()
        {
            string username = this.getUsername(HttpContext);
            ViewData["user"] = _context.User.Where(a => a.Username == username).SingleOrDefault();
            ViewData["keys"] = await _context.Key.Where(a => a.UserKeyId == this.getUserId(HttpContext)).ToListAsync();
            return View();
        }
    }
}
