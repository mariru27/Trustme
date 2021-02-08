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
        public IEnumerable<Key> getAllKeys(HttpContext httpContext);
        public bool isloggedIn(HttpContext httpcontext);
        public string getUsername(HttpContext httpcontext);
        public int getUserId(HttpContext httpcontext);
    }
}
