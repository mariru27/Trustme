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

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register([Bind("UserId,firstName,secondName,mail,username,password,confirmPassword")] User user)
        {
            User usedUser = _context.User.Where(a => a.username == user.username)?.SingleOrDefault();
            if (usedUser != null)
            {
                ModelState.AddModelError("", "Try another username, this username already is used");
                return View(user);
            }
            if (ModelState.IsValid && user.password == user.confirmPassword)
            {

                _context.Add(user);
                await _context.SaveChangesAsync();
                return  await LogIn(user.username, user.password);
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

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("LogIn");
        }

        public string getPublicKey(HttpContext httpcontext)
        {
            var username = this.getUsername(httpcontext);
            User user = _context.User.Where(a => a.username == username)?.SingleOrDefault();
            Key key = _context.Key.Where(a => a.UserId == user.UserId)?.SingleOrDefault();
            return key.PublicKey;

        }
        public string getPublicKey(string username)
        {
            User user = _context.User.Where(a => a.username == username)?.SingleOrDefault();
            Key key = _context.Key.Where(a => a.UserId == user.UserId)?.SingleOrDefault();
            return key.PublicKey;

        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public  bool isloggedIn(HttpContext httpcontext)
        {
            var username = httpcontext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (username != null)
                return true;
            return false;
        }

        public string getUsername(HttpContext httpcontext)
        {
            return httpcontext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public  void addPublicKey(string username, string publicKey)
        {
            User user =  _context.User.Where(a => a.username == username)?.SingleOrDefault();
            Key userKey = _context.Key.Where(a => a.UserId == user.UserId)?.SingleOrDefault();
            if (userKey == null)
            {
                Key newKey = new Key();
                newKey.UserId = user.UserId;
                newKey.PublicKey = publicKey;
                 _context.Add(newKey);
            }
            else
            {
                userKey.PublicKey = publicKey;
            }
             _context.SaveChanges();
        }

    }
}
