using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Trustme.Data;
using Trustme.Models;
using AppContext = Trustme.Data.AppContext;

namespace Trustme.Controllers
{
    public class Administration : Controller
    {
        private readonly AppContext _context;
        public Administration(AppContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register([Bind("UserId,firstName,secondName,mail,username,password,confirmPassword")] User user)
        {
            if(ModelState.IsValid && user.password == user.confirmPassword)
            {

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ModelState.AddModelError("", "could not log in");
            return View(user);
            
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
            User user = _context.User.Where(a => a.username == username).SingleOrDefault();
            if (user != null && password == user.password)
            {
                var userClaim = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.username),
                    new Claim(ClaimTypes.Email, user.mail),
                };
                var userIdentity = new ClaimsIdentity(userClaim, "user identity");
                
                var userPrinciple = new ClaimsPrincipal(new[] { userIdentity });
                
                await HttpContext.SignInAsync(userPrinciple);
                ViewData["username"] = user.username;
                            
                return RedirectToAction("Index");
            }
            return RedirectToAction("LogIn");
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        public  bool isloggedIn(HttpContext httpcontext)
        {
            var username = httpcontext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (username != null)
                return true;
            return false;
        }

    }
}
