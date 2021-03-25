using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Trustme.IServices;
using Trustme.Models;


namespace Trustme.Service
{
    [Authorize]
    public class HttpRequestFunctions : IHttpRequestFunctions
    {

        private IKeyRepository _KeyRepository;
        private IUserRepository _UserRepository;
        public HttpRequestFunctions(IKeyRepository keyRepository, IUserRepository userRepository)
        {
            _KeyRepository = keyRepository;
            _UserRepository = userRepository;
        }
        public IEnumerable<Key> GetAllKeys(HttpContext httpContext)
        {
            User user = _UserRepository.GetUserById(this.GetUserId(httpContext));
            var appContext = _KeyRepository.ListAllKeys(user).AsEnumerable();
            return appContext;
        }


        public bool IsloggedIn(HttpContext httpcontext)
        {
            var username = httpcontext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (username != null)
                return true;
            return false;
        }



        public string GetUsername(HttpContext httpcontext)
        {
            return httpcontext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public int GetUserId(HttpContext httpcontext)
        {
            string username = this.GetUsername(httpcontext);
            User user = _UserRepository.GetUserbyUsername(username);
            return user.UserId;

        }

        public string GetUserRole(HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;            
        }

        public User GetUser(HttpContext httpContext)
        {
            int userId = this.GetUserId(httpContext);
            return _UserRepository.GetUserById(userId);
        }


    }
}
