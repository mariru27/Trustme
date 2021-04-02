﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Trustme.IServices;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    /// <summary>
    /// Admin user type can delete, 
    /// edit user info
    /// </summary>

    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        private readonly IRoleRepository _RoleRepository;
        private readonly IUserRepository _UserRepository;
        public AdministratorController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _RoleRepository = roleRepository;
            _UserRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Users()
        {
            IEnumerable<User> users = _UserRepository.ListAllUsers();

            //Add in list all users and role for every user
            List<RoleUserModel> usersroles = new List<RoleUserModel>();
            foreach (var u in users)
            {
                RoleUserModel roleUser = new RoleUserModel
                {
                    User = u,
                    Role = _RoleRepository.GetUserRole(u)
                };
                usersroles.Add(roleUser);
            }
            return View(usersroles);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteConfirmed(int? UserId)
        {
            if (UserId == null)
                return NotFound();
            User user = _UserRepository.GetUserById((int)UserId);
            _UserRepository.DeleteUser(user);
            return RedirectToAction(nameof(Users));
        }

        [HttpPost]
        public IActionResult EditUser(EditUserModel user)
        {
            EditUserRolesModel roleUser = new EditUserRolesModel
            {
                User = user,
                Roles = new SelectList(_RoleRepository.ListAllRoles().Where(a => a.RoleName != "Admin").ToList(), "IdRole", "RoleName")
            };
            //Verify model state
            if (!ModelState.IsValid)
            {
                return View(roleUser);
            }
            User userFromDatabase = _UserRepository.GetUserById(user.UserId);
            //Verify if user already exist
            if (_UserRepository.UsernameExist(user.Username) == true && userFromDatabase.Username != user.Username)
            {
                ModelState.AddModelError("", "Username is already used");
                return View(roleUser);
            }

            User updateUser = _UserRepository.GetUserById(user.UserId);

            updateUser.Update(user);

            _UserRepository.EditUser(updateUser);
            return RedirectToAction("Users");
        }

        [HttpGet]
        public IActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = _UserRepository.GetUserById((int)id);
            if (user == null)
                return NotFound();
            EditUserRolesModel roleUser = new EditUserRolesModel
            {
                User = new EditUserModel(user),
                Roles = new SelectList(_RoleRepository.ListAllRoles().Where(a => a.RoleName != "Admin").ToList(), "IdRole", "RoleName")
            };

            return View(roleUser);
        }

        public IActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = _UserRepository.GetUserById((int)id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
