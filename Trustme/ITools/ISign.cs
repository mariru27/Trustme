using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.ITools
{
    public interface ISign
    {
        public bool SignDoc(IFormFile pkfile, IFormFile docfile, int certificates, HttpContext httpContext);

    }
}
