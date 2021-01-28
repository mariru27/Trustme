using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Trustme.Data;
using Trustme.Models;
using AppContext = Trustme.Data.AppContext;
using Trustme.ViewModels;
using Trustme.Service;
using Trustme.IServices;

namespace Trustme.Controllers
{
    public class Administration : Controller
    {
        private readonly AppContext _context;
        private string username;
        ///private GuestServiceRepository _guestServiceRepository;
        public Administration(AppContext context)
        {
            _context = context;
            //_guestServiceRepository = guestServiceRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            string username = this.getUsername(HttpContext);
            ViewData["user"] = _context.User.Where(a => a.Username == username).SingleOrDefault();
            ViewData["keys"] = await _context.Key.Where(a => a.UserId == this.getUserId(HttpContext)).ToListAsync();
            return View();
        }

        public IEnumerable<Key> getAllKeys(HttpContext httpContext)
        {
            var appContext = _context.Key.Where(k => k.UserId == this.getUserId(httpContext)).AsEnumerable();
            return appContext;
        }
        public IEnumerable<Key> getAllKeysByUsername(string username)
        {
            int? id = getUserId(username);
            if (id == null)
                return null;
            var appContext = _context.Key.Where(k => k.UserId == id).AsEnumerable();
            return appContext;
        }

        public IActionResult DeleteCertificate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var key = getKey(this.getUserId(HttpContext), (int)id);
            if (key == null)
            {
                return NotFound();
            }

            return View(key);
        }

        [HttpPost, ActionName("DeleteCertificate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var key = this.getKey(this.getUserId(HttpContext), id);
            _context.Key.Remove(key);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Profile));
        }
        public Key getKey(int userId, int keyId)
        {
            return _context.Key.Where(a => a.UserId == userId && a.KeyId == keyId).SingleOrDefault();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Key key)
        {
            if (id != key.KeyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(key);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeyExists(key.UserId, key.KeyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Profile));
            }
            ViewData["user"] = _context.User.Where(a => a.UserId == key.UserId).SingleOrDefault();
            return View(key);
        }
        private bool KeyExists(int idUser, int idKey)
        {
            return _context.Key.Any(e => e.UserId == idUser && e.KeyId == idKey);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Key key = _context.Key.Where(a => a.UserId == this.getUserId(HttpContext) && a.KeyId == id).SingleOrDefault();
            if (key == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", key.UserId);
            return View(key);
        }

        public User getUserByUsername(string username)
        {
            return  _context.User.Where(a => a.Username == username)?.SingleOrDefault();
        }

        public User getUserbyId(int id)
        {
            return  _context.User.Where(a => a.UserId == id)?.SingleOrDefault();
        }



        public IActionResult Register()
        {
            RolesUserViewModel rolesUserViewModel = new RolesUserViewModel();
            rolesUserViewModel.Roles = new SelectList(_context.Role.ToList(),"IdRole","RoleName");
            return View(rolesUserViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(User user)
        {
            User usedUser = _context.User.Where(a => a.Username == user.Username)?.FirstOrDefault();
            User usedMailUser = _context.User.Where(a => a.Mail == user.Mail)?.FirstOrDefault();

            RolesUserViewModel userResult = new RolesUserViewModel();
            userResult.User = user;
            userResult.Roles = new SelectList(_context.Role.ToList(), "IdRole", "RoleName");

            if (usedUser != null || usedMailUser != null)
            {
                if(usedMailUser != null)
                {
                    ModelState.AddModelError("", "Try another mail, this mail already is used");
                }else
                {

                    ModelState.AddModelError("", "Try another username, this username already is used");
                }
                return View(userResult);
            }
            if (ModelState.IsValid && user.Password == user.ConfirmPassword)
            {
                Role role = _context.Role.Where(a => a.IdRole == user.RoleId).SingleOrDefault();
                user.Role = role;
                //from here I can use GuestServiceRepository

                //_guestServiceRepository.Register(user);

                role.Users.Add(user);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return  await LogIn(user.Username, user.Password);
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
            User user = _context.User.Where(a => a.Username == username).SingleOrDefault();
            if (user != null && password == user.Password)
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

        public string getPublicKeyByCertificateName(string username, string certificateName)
        {
            User user = _context.User.Where(a => a.Username == username)?.SingleOrDefault();
            Key key = _context.Key.Where(a => a.UserId == user.UserId && a.CertificateName == certificateName)?.SingleOrDefault();
            
            return key.PublicKey;
        }

        public string getPublicKey(HttpContext httpcontext)
        {
            var username = this.getUsername(httpcontext);
            User user = _context.User.Where(a => a.Username == username)?.SingleOrDefault();
            Key key = _context.Key.Where(a => a.UserId == user.UserId)?.SingleOrDefault();
            return key.PublicKey;

        }
        public User getUserbyId(string username)
        {
            User user = _context.User.Where(a => a.Username == username)?.SingleOrDefault();
            return user;
        }
        public string getPublicKey(HttpContext httpcontext, int certificateId)
        {
            var username = this.getUsername(httpcontext);
            Key key = _context.Key.Where(a => a.UserId == this.getUserId(httpcontext) && a.KeyId == certificateId).SingleOrDefault();
            return key.PublicKey;

        }
        public string getPublicKey(string username)
        {
            User user = _context.User.Where(a => a.Username == username)?.SingleOrDefault();
            Key key = _context.Key.Where(a => a.UserId == user.UserId)?.SingleOrDefault();
            return key.PublicKey;

        }


        [Authorize]
        public async Task<IActionResult> Secret()
        {
            
            string username = this.getUsername(HttpContext);
            ViewData["user"] = _context.User.Where(a => a.Username == username).SingleOrDefault();
            ViewData["keys"] = await _context.Key.Where(a => a.UserId == this.getUserId(HttpContext)).ToListAsync();
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

        public  void addPublicKey(string username, string publicKey, string certificateName, string description, int keySize)
        {
            //find user
            User user =  _context.User.Where(a => a.Username == username)?.SingleOrDefault();
            
            Key userKey = _context.Key.Where(a => a.UserId == user.UserId && a.CertificateName == certificateName)?.SingleOrDefault();

            if (userKey == null)
            {
                Key newKey = new Key();
                newKey.UserId = user.UserId;
                newKey.PublicKey = publicKey;
                newKey.CertificateName = certificateName;
                newKey.Description = description;
                newKey.KeySize = keySize;

                //user.Keys.Add(newKey);
                
                _context.Add(newKey);
            }
             _context.SaveChanges();
        }

        public void editPublicKey(string username, string publicKey, string certificateName)
        {
            User user = _context.User.Where(a => a.Username == username)?.SingleOrDefault();
            Key userKey = _context.Key.Where(a => a.UserId == user.UserId && a.CertificateName == certificateName)?.SingleOrDefault();

            if (userKey != null)
            {
                userKey.PublicKey = publicKey;

            }
            _context.SaveChanges();
        }
        public void deletePublicKey(string username, string certificateName)
        {
            User user = _context.User.Where(a => a.Username == username)?.SingleOrDefault();
            Key userKey = _context.Key.Where(a => a.UserId == user.UserId && a.CertificateName == certificateName)?.SingleOrDefault();
            _context.Key.Remove(userKey);
            _context.SaveChanges();
        }

        public int? getUserId(string username)
        {
            User user = _context.User.Where(a => a.Username == username)?.FirstOrDefault();
            if (user == null)
                return null;
            return user.UserId;

        }
        public int getUserId(HttpContext httpcontext)
        {
            string username = this.getUsername(httpcontext);
            User user = _context.User.Where(a => a.Username == username)?.FirstOrDefault();
            return user.UserId;

        }

    }
}
