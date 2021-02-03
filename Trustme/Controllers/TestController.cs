using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trustme.Data;
using Trustme.Models;
using Trustme.ViewModels;
using Trustme.IServices;
using Trustme.Service;

namespace Trustme.Controllers
{
    public class TestController : Controller
    {
        private IKeyRepository _KeyRepository;
        private IRoleRepository _RoleRepository;
        private IUserRepository _UserRepository;
        TestController(IKeyRepository keyRepository, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _KeyRepository = keyRepository;
            _RoleRepository = roleRepository;
            _UserRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public void TestAction()
        {

        }
    }
}
