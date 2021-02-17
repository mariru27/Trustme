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
    [Authorize]

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCertificate(int id, Key key)
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

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_KeyRepository.KeyExists(key.KeyId, key.KeyId))
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
