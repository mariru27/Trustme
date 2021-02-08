﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trustme.IServices;
using Trustme.Service;
using Trustme.Models;
using Trustme.Data;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    public class Test1Controller : Controller
    {
        private IKeyRepository _keyRepository;
        public Test1Controller(IKeyRepository keyRepository)
        {
            _keyRepository = keyRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public void test()
        {
            UserKeyModel userKeyModel = new UserKeyModel();
            userKeyModel = _keyRepository.CreateDefaultUserKeyModel();
            
            _keyRepository.AddKey(userKeyModel);
        }
    }
}