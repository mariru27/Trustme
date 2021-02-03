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
        private KeyRepository _KeyRepository;
        private RoleRepository _RoleRepository;
        private UserRepository _UserRepository;
        TestController(KeyRepository keyRepository, RoleRepository roleRepository, UserRepository userRepository)
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
