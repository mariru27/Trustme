using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.IServices;
using Trustme.Models;


namespace Trustme.Service
{
    public class HttpRequestFunctions
    {

        private IKeyRepository _KeyRepository;
        private IUserRepository _UserRepository;
        public HttpRequestFunctions(IKeyRepository keyRepository, IUserRepository userRepository)
        {
            _KeyRepository = keyRepository;
            _UserRepository = userRepository;
        }
        public IEnumerable<Key> getAllKeys(HttpContext httpContext)
        {
            User user = _UserRepository.GetUserById(this.getUserId(httpContext));
            var appContext = _KeyRepository.ListAllKeys(user).AsEnumerable();
            return appContext;
        }

        public string getPublicKey(HttpContext httpcontext)
        {
            var username = this.getUsername(httpcontext);
            User user = _context.User.Where(a => a.Username == username)?.SingleOrDefault();
            Key key = _context.Key.Where(a => a.UserKeyId == user.UserId)?.SingleOrDefault();
            return key.PublicKey;

        }

        public string getPublicKey(HttpContext httpcontext, int certificateId)
        {
            var username = this.getUsername(httpcontext);
            Key key = _context.Key.Where(a => a.UserKeyId == this.getUserId(httpcontext) && a.KeyId == certificateId).SingleOrDefault();
            return key.PublicKey;

        }

        public bool isloggedIn(HttpContext httpcontext)
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

        public int getUserId(HttpContext httpcontext)
        {
            string username = this.getUsername(httpcontext);
            User user = _context.User.Where(a => a.Username == username)?.FirstOrDefault();
            return user.UserId;

        }
    }
}
