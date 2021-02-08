using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Service
{
    public class HttpRequestFunctions
    {
        public IEnumerable<Key> getAllKeys(HttpContext httpContext)
        {
            var appContext = _context.Key.Where(k => k.UserKeyId == this.getUserId(httpContext)).AsEnumerable();
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
