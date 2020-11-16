using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //var rpassword = Request.Form["rpassword"].ToString();
            if(ModelState.IsValid && user.password == user.confirmPassword)
            {

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
            
        }

        public async Task<IActionResult> Registertest(string rpassword)
        {
            var user = new User();
            //var rpassword = Request.Form["rpassword"].ToString();
            //user.mail = Request.Form["mail"].ToString();
            //user.secondName = Request.Form["secondName"].ToString();
            //user.password = Request.Form["password"].ToString();
            user.firstName = rpassword;
            _context.Add(user);
                await _context.SaveChangesAsync();
            //if(user.password == rpassword)
            //{
            //    //return RedirectToAction(nameof(Index));
            //}
            //return View(user);
            return RedirectToAction("Index");
        }


    }
}
