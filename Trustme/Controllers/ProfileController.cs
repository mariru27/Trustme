using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Data;
using Trustme.IServices;
using Trustme.Service;
using Trustme.ViewModels;
using Trustme.Models;
using Microsoft.EntityFrameworkCore;

namespace Trustme.Controllers
{
    //change phpto, edit all values, display
    //edit, delete key
    public class ProfileController : Controller
    {
        private IHttpRequestFunctions _HttpRequestFunctions;
        private IUserRepository _UserRepository;
        private IKeyRepository _KeyRepository;
        private IRoleRepository _RoleRepository;
        public ProfileController(IHttpRequestFunctions httpRequestFunctions, IKeyRepository keyRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            _KeyRepository = keyRepository;
            _UserRepository = userRepository;
            _RoleRepository = roleRepository;
            
        }
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Profile()
        {
            string username = _HttpRequestFunctions.GetUsername(HttpContext);
            UserKeysRoleModel userKeysRoleModel = new UserKeysRoleModel
            {
                User = _UserRepository.GetUserbyUsername(username),
                Keys = _KeyRepository.ListAllKeys(_UserRepository.GetUserbyUsername(username)),
                Role = _RoleRepository.GetUserRole(_UserRepository.GetUserbyUsername(username)) 
            };
            return View(userKeysRoleModel);
        }

    
        //public void addPublicKey(string username, string publicKey, string certificateName, string description, int keySize)
        //{
        //    //find user
        //    User user = _context.User.Where(a => a.Username == username)?.SingleOrDefault();

        //    Key userKey = _context.Key.Where(a => a.UserKeyId == user.UserId && a.CertificateName == certificateName)?.SingleOrDefault();

        //    if (userKey == null)
        //    {
        //        Key newKey = new Key();
        //        newKey.UserKeyId = user.UserId;
        //        newKey.PublicKey = publicKey;
        //        newKey.CertificateName = certificateName;
        //        newKey.Description = description;
        //        newKey.KeySize = keySize;

        //        //user.Keys.Add(newKey);

        //        _context.Add(newKey);
        //    }
        //    _context.SaveChanges();
        //}
        //public void deletePublicKey(string username, string certificateName)
        //{

        //    User user = _context.User.Where(a => a.Username == username)?.SingleOrDefault();
        //    Key userKey = _context.Key.Where(a => a.UserKeyId == user.UserId && a.CertificateName == certificateName)?.SingleOrDefault();
        //    _context.Key.Remove(userKey);
        //    _context.SaveChanges();
        //}

        public IActionResult DeleteCertificate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var key = _KeyRepository.GetKey(_HttpRequestFunctions.GetUserId(HttpContext), (int)id);
            if (key == null)
            {
                return NotFound();
            }

            return View(key);
        }

        [HttpPost, ActionName("DeleteCertificate")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            UserKeyModel userKeyModel = new UserKeyModel
            {
                Key = _KeyRepository.GetKey(_HttpRequestFunctions.GetUserId(HttpContext), (int)id),
                User = _HttpRequestFunctions.GetUser(HttpContext)
            };

            _KeyRepository.DeleteKey(userKeyModel);
            return RedirectToAction(nameof(Profile));
        }




        //public void editPublicKey(string username, string publicKey, string certificateName)
        //{
        //    User user = _context.User.Where(a => a.Username == username)?.SingleOrDefault();
        //    Key userKey = _context.Key.Where(a => a.UserKeyId == user.UserId && a.CertificateName == certificateName)?.SingleOrDefault();

        //    if (userKey != null)
        //    {
        //        userKey.PublicKey = publicKey;

        //    }
        //    _context.SaveChanges();
        //}

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
                    UserKeyModel userKeyModel = new UserKeyModel
                    {
                        User = _HttpRequestFunctions.GetUser(HttpContext),
                        Key = key
                    };

                    _KeyRepository.UpdateKey(userKeyModel);
                    //_context.Update(key);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeyExists(key.UserKeyId, key.KeyId))
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
            ViewData["user"] = _context.User.Where(a => a.UserId == key.UserKeyId).SingleOrDefault();
            return View(key);
        }

        public IActionResult EditCertificate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Key key = _KeyRepository.GetKey(_HttpRequestFunctions.GetUserId(HttpContext),(int) id);
            if (key == null)
            {
                return NotFound();
            }
            return View(key);
        }

    }
}
