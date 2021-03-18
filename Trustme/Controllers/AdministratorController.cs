﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trustme.IServices;
using Trustme.Service;

namespace Trustme.Controllers
{
    /// <summary>
    /// Admin user type can delete, 
    /// edit user info
    /// </summary>
    
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        private IUserRepository _UserRepository;
        public AdministratorController(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Users()
        {

            return View(_UserRepository.ListAllUsers());
        }
    }
}