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
                return RedirectToAction(nameof(LogIn));
            }
            return View(user);
            
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult LogIn(string username, string password)
        {
            User user = _context.User.Where(a => a.username == username).SingleOrDefault();
            if (user != null && password == user.password)
            {
                var userClaim = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, username),
                };
                var userIdentity = new ClaimsIdentity(userClaim, "user identity");

                var userPrinciple = new ClaimsPrincipal(new[] { userIdentity });

                HttpContext.SignInAsync(userPrinciple);

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

        public  bool checkAuthentification()
        {

            bool result = User.Identity.IsAuthenticated;


            return result;
            
        }

    }
}
