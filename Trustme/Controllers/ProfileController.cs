using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Data;
using Trustme.IServices;
using Trustme.Service;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    //change phpto, edit all values, display
    public class ProfileController : Controller
    {
        private IHttpRequestFunctions _HttpRequestFunctions;
        private IUserRepository _UserRepository;
        private IKeyRepository _KeyRepository;
        private IRoleRepository _RoleRepository;
        public ProfileController(IHttpRequestFunctions httpRequestFunctions, IKeyRepository keyRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            _KeyRepository = keyRepository;
            _UserRepository = userRepository;
            _RoleRepository = roleRepository;
            
        }
        public IActionResult Index()
        {
            return View();
        }


       // [Authorize]
        public IActionResult Profile()
        {
            string username = _HttpRequestFunctions.getUsername(HttpContext);
            UserKeysRoleModel userKeysRoleModel = new UserKeysRoleModel
            {
                User = _UserRepository.GetUserbyUsername(username),
                Keys = _KeyRepository.ListAllKeys(_UserRepository.GetUserbyUsername(username)),
                Role = _RoleRepository.GetUserRole(_UserRepository.GetUserbyUsername(username)) 
            };
            return View(userKeysRoleModel);
        }
    }
}
