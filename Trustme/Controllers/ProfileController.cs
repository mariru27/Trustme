using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Data;
using Trustme.IServices;
using Trustme.Service;

namespace Trustme.Controllers
{
    //change phpto, edit all values, display
    public class ProfileController : Controller
    {
        private IHttpRequestFunctions _HttpRequestFunctions;
        private IUserRepository _UserRepository;
        private IKeyRepository _KeyRepository;
        public ProfileController(IHttpRequestFunctions httpRequestFunctions, IKeyRepository keyRepository, IUserRepository userRepository)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            _KeyRepository = keyRepository;
            _UserRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }


       // [Authorize]
        public IActionResult Profile()
        {
            string username = _HttpRequestFunctions.getUsername(HttpContext);
            ViewData["user"] = _UserRepository.GetUserbyUsername(username);
            ViewData["keys"] = _KeyRepository.ListAllKeys(_UserRepository.GetUserbyUsername(username));
            return View();
        }
    }
}
