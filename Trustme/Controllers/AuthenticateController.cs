using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Trustme.IServices;
using Trustme.ITools;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    //register, login
    public class AuthenticateController : Controller
    {
        private readonly IRoleRepository _RoleReporitory;
        private readonly IUserRepository _UserRepository;
        private readonly ICrypto _Tool;
        private readonly IEmailSender _EmailSender;
        private readonly IJwtAuthenticationManager _JwtAuthenticationManager;
        public AuthenticateController(IRoleRepository roleRepository, IUserRepository userRepository, ICrypto tool,
                                      IEmailSender emailSender, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _UserRepository = userRepository;
            _RoleReporitory = roleRepository;
            _Tool = tool;
            _EmailSender = emailSender;
            _JwtAuthenticationManager = jwtAuthenticationManager;
        }

        public IActionResult Register()
        {
            RolesUserViewModel rolesUserViewModel = new RolesUserViewModel
            {
                Roles = new SelectList(_RoleReporitory.ListAllRoles().Where(a => a.RoleName != "Admin").ToList(), "IdRole", "RoleName")
            };
            return View(rolesUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                RolesUserViewModel rolesUserViewModel = new RolesUserViewModel
                {
                    Roles = new SelectList(_RoleReporitory.ListAllRoles().Where(a => a.RoleName != "Admin").ToList(), "IdRole", "RoleName"),
                    User = new User()
                };
                return View(rolesUserViewModel);
            }
            User usedUser = _UserRepository.GetUserbyUsername(user.Username);
            User usedMailUser = _UserRepository.GetUserbyMail(user.Mail);

            RolesUserViewModel userResult = new RolesUserViewModel
            {
                User = user,
                Roles = new SelectList(_RoleReporitory.ListAllRoles().Where(a => a.RoleName != "Admin").ToList(), "IdRole", "RoleName")
            };

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
                Login login = new Login { Username = user.Username, Password = user.Password };
                user.Password = _Tool.ComputeHash(user.Password, new SHA256CryptoServiceProvider());
                user.ConfirmPassword = _Tool.ComputeHash(user.ConfirmPassword, new SHA256CryptoServiceProvider());
                Role role = _RoleReporitory.GetRoleById(user.RoleId);
                user.Role = role;


                string token = _JwtAuthenticationManager.GenerateTokenForUser(user);

                //send confirmation email
                SendMailModel sendMailModel = new SendMailModel
                {
                    ToUsername = user.Username,
                    ToUserMail = user.Mail,
                    MessageSubject = "Trustme application",
                    MessageBodyHtml = "If you created this account click on this link for confirmation: " + "https://localhost:44318/Authenticate/EmailConfirmation?username=" + user.Username + "&&token=" + token,
                };

                _EmailSender.SendMail(sendMailModel);
                user.Token = token;
                _UserRepository.AddUser(user);
                return RedirectToAction("EmailConfirmationMessage", new { Username = user.Username });
            }
            return View(userResult);
        }

        [HttpGet]
        public IActionResult EmailConfirmation(string username, string token)
        {
            User user = _UserRepository.GetUserbyUsername(username);
            if (user.Token == token)
            {
                user.VerifiedAccount = true;
                _UserRepository.UpdateUser(user);
                TempData["CreatedAccount"] = "Your account was created successfully, you can login!";
            }

            return RedirectToAction("LogIn");
        }

        [HttpGet]
        public IActionResult EmailConfirmationMessage()
        {

            return View();
        }


        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> LogIn(Login login)
        {
            if (isloggedIn(HttpContext) == true)
                await LogOut();
            if (!ModelState.IsValid) { return View(); }



            User user = _UserRepository.GetUserbyUsername(login.Username);
            string hashPassword = _Tool.ComputeHash(login.Password, new SHA256CryptoServiceProvider());
            if (user != null && hashPassword == user.Password)
            {
                if (user.VerifiedAccount == false)
                {
                    ModelState.AddModelError("", "Confirm your email");
                    return View();
                }
                Role userRole = _RoleReporitory.GetUserRole(user);

                var userClaim = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Email, user.Mail),
                        new Claim(ClaimTypes.Role, userRole.RoleName)
                    };
                var userIdentity = new ClaimsIdentity(userClaim, "user identity");

                var userPrinciple = new ClaimsPrincipal(new[] { userIdentity });

                await HttpContext.SignInAsync(userPrinciple);
                ViewData["username"] = user.Username;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "User or password are incorrect");
            }
            return View();
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
