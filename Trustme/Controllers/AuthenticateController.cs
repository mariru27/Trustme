using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trustme.Data;
using Trustme.ViewModels;
using Trustme.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Trustme.Controllers
{   
    //register, login
    public class AuthenticateController : Controller
    {
        private readonly AppContext _context;

        //AuthenticateController(AppContext context)
        //{
        //    _context = context;
        //}
        //public IActionResult Register()
        //{
        //    RolesUserViewModel rolesUserViewModel = new RolesUserViewModel();
        //    rolesUserViewModel.Roles = new SelectList(_context.Role.ToList(), "IdRole", "RoleName");
        //    return View(rolesUserViewModel);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public async Task<IActionResult> Register(User user)
        //{
        //    User usedUser = _context.User.Where(a => a.Username == user.Username)?.FirstOrDefault();
        //    User usedMailUser = _context.User.Where(a => a.Mail == user.Mail)?.FirstOrDefault();

        //    RolesUserViewModel userResult = new RolesUserViewModel();
        //    userResult.User = user;
        //    userResult.Roles = new SelectList(_context.Role.ToList(), "IdRole", "RoleName");

        //    if (usedUser != null || usedMailUser != null)
        //    {
        //        if (usedMailUser != null)
        //        {
        //            ModelState.AddModelError("", "Try another mail, this mail already is used");
        //        }
        //        else
        //        {

        //            ModelState.AddModelError("", "Try another username, this username already is used");
        //        }
        //        return View(userResult);
        //    }
        //    if (ModelState.IsValid && user.Password == user.ConfirmPassword)
        //    {
        //        Role role = _context.Role.Where(a => a.IdRole == user.RoleId).SingleOrDefault();
        //        user.Role = role;
        //        //from here I can use GuestServiceRepository

        //        //_guestServiceRepository.Register(user);

        //        //role.Users.Add(user);
        //        _context.Add(user);
        //        await _context.SaveChangesAsync();
        //        return await LogIn(user.Username, user.Password);
        //    }
        //    return View(userResult);
        //}



        //[HttpGet]
        //public IActionResult LogIn()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public async Task<IActionResult> LogIn(string username, string password)
        //{
        //    if (isloggedIn(HttpContext) == true)
        //        await LogOut();
        //    User user = _context.User.Where(a => a.Username == username).SingleOrDefault();
        //    if (user != null && password == user.Password)
        //    {
        //        var userClaim = new List<Claim>()
        //        {
        //            new Claim(ClaimTypes.NameIdentifier, user.Username),
        //            new Claim(ClaimTypes.Email, user.Mail),
        //        };
        //        var userIdentity = new ClaimsIdentity(userClaim, "user identity");

        //        var userPrinciple = new ClaimsPrincipal(new[] { userIdentity });

        //        await HttpContext.SignInAsync(userPrinciple);
        //        ViewData["username"] = user.Username;

        //        return RedirectToAction("Index", "Home");
        //    }
        //    return RedirectToAction("LogIn");
        //}


        //public bool isloggedIn(HttpContext httpcontext)
        //{
        //    var username = httpcontext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        //    if (username != null)
        //        return true;
        //    return false;
        //}
        //public async Task<IActionResult> LogOut()
        //{
        //    await HttpContext.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
