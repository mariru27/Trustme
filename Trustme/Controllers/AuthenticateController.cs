using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trustme.ViewModels;
using Trustme.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Trustme.IServices;
using Trustme.ITools;
using System.Security.Cryptography;

namespace Trustme.Controllers
{   
    //register, login
    public class AuthenticateController : Controller
    {
        //private readonly AppContext _context;
        private IKeyRepository _KeyRepository;
        private IRoleRepository _RoleReporitory;
        private IUserRepository _UserRepository;
        private ITool _Tool;
        public AuthenticateController(IKeyRepository keyRepository, IRoleRepository roleRepository, IUserRepository userRepository, ITool tool)
        {
            _UserRepository = userRepository;
            _KeyRepository = keyRepository;
            _RoleReporitory = roleRepository;
            _Tool = tool;
        }

        public IActionResult Register()
        {
            RolesUserViewModel rolesUserViewModel = new RolesUserViewModel();
            rolesUserViewModel.Roles = new SelectList(_RoleReporitory.ListAllRoles(), "IdRole", "RoleName");
            return View(rolesUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(User user)
        {
            string password = user.Password;
            User usedUser = _UserRepository.GetUserbyUsername(user.Username);
            User usedMailUser = _UserRepository.GetUserbyMail(user.Mail);

            RolesUserViewModel userResult = new RolesUserViewModel();
            userResult.User = user;
            userResult.Roles = new SelectList(_RoleReporitory.ListAllRoles(), "IdRole", "RoleName");

            if (usedUser != null || usedMailUser != null)
            {
                if (usedMailUser != null)
                {
                    ModelState.AddModelError("", "Try another mail, this mail already is used");
                }
                else
                {

                    ModelState.AddModelError("", "Try another username, this username already is used");
                }
                return View(userResult);
            }

            //hash password 

            if (ModelState.IsValid && user.Password == user.ConfirmPassword)
            {
                user.Password = _Tool.ComputeHash(user.Password, new SHA256CryptoServiceProvider());
                user.ConfirmPassword = _Tool.ComputeHash(user.ConfirmPassword, new SHA256CryptoServiceProvider());
                Role role = _RoleReporitory.GetRoleById(user.RoleId);
                user.Role = role;
                _UserRepository.AddUser(user);
                return await LogIn(user.Username, password);
            }
            return View(userResult);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> LogIn(string username, string password)
        {
            if (isloggedIn(HttpContext) == true)
                await LogOut();
            if(username == null)
            {
                TempData["UsernameRequired"] = "Username field is required!";
            }
            if(password == null)
            {
                TempData["PasswordRequired"] = "Password field is required";
            }
            User user = _UserRepository.GetUserbyUsername(username);
            string hashPassword = _Tool.ComputeHash(password, new SHA256CryptoServiceProvider());
            if (user != null && hashPassword == user.Password)
            {
                var userClaim = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Email, user.Mail),
                    };
                var userIdentity = new ClaimsIdentity(userClaim, "user identity");

                var userPrinciple = new ClaimsPrincipal(new[] { userIdentity });

                await HttpContext.SignInAsync(userPrinciple);
                ViewData["username"] = user.Username;

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("LogIn");
        }

        public bool isloggedIn(HttpContext httpcontext)
        {
            var username = httpcontext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (username != null)
                return true;
            return false;
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
