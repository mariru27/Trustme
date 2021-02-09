using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.IServices
{
    public interface IHttpRequestFunctions
    {
        public IEnumerable<Key> GetAllKeys(HttpContext httpContext);
        public bool IsloggedIn(HttpContext httpcontext);
        public string GetUsername(HttpContext httpcontext);
        public int GetUserId(HttpContext httpcontext);
    }
}
