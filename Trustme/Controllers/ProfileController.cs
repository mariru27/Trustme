using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trustme.IServices;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Controllers
{
    [Authorize]

    public class ProfileController : Controller
    {
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        private readonly IUserRepository _UserRepository;
        private readonly IKeyRepository _KeyRepository;
        private readonly IRoleRepository _RoleRepository;
        public ProfileController(IHttpRequestFunctions httpRequestFunctions, IKeyRepository keyRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            _KeyRepository = keyRepository;
            _UserRepository = userRepository;
            _RoleRepository = roleRepository;

        }
        public IActionResult UserProfile()
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
            return RedirectToAction(nameof(UserProfile));
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

                }
                return RedirectToAction(nameof(UserProfile));
            }
            return View(key);
        }

        public IActionResult EditCertificate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Key key = _KeyRepository.GetKey(_HttpRequestFunctions.GetUserId(HttpContext), (int)id);
            if (key == null)
            {
                return NotFound();
            }
            return View(key);
        }

    }
}
