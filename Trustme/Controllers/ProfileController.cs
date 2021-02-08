using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trustme.Data;
using Trustme.IServices;
using Trustme.Service;
using Trustme.Models;

namespace Trustme.Controllers
{
    //change phpto, edit all values, display
    public class ProfileController : Controller
    {
        private HttpRequestFunctions _HttpRequestFunctions;
        private IUserRepository _UserRepository;
        private IKeyRepository _KeyRepository;
        public ProfileController(HttpRequestFunctions httpRequestFunctions, IKeyRepository keyRepository, IUserRepository userRepository)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            _KeyRepository = keyRepository;
            _UserRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Profile()
        {
            string username = _HttpRequestFunctions.getUsername(HttpContext);
            ViewData["user"] = _UserRepository.GetUserbyUsername(username);
            ViewData["keys"] = _KeyRepository.ListAllKeys(_UserRepository.GetUserbyUsername(username));
            return View();
        }
    }
}
