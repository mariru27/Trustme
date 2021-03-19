﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trustme.IServices;
using Trustme.Models;
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

        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? UserId)
        {
            if (UserId == null)
                return NotFound();
            User user = _UserRepository.GetUserById((int)UserId);
            _UserRepository.DeleteUser(user);
            return RedirectToAction(nameof(Users));
        }

        [HttpGet]
        public IActionResult EditUser()
        {
            return View();
        }
        public IActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = _UserRepository.GetUserById((int)id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
