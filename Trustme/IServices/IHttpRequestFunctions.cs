using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Trustme.Models;

namespace Trustme.IServices
{
    public interface IHttpRequestFunctions
    {
        public IEnumerable<Key> GetAllKeys(HttpContext httpContext);
        public bool IsloggedIn(HttpContext httpcontext);
        public string GetUsername(HttpContext httpcontext);
        public int GetUserId(HttpContext httpcontext);

        public User GetUser(HttpContext httpContext);
        public string GetUserRole(HttpContext httpContext);

    }
}
