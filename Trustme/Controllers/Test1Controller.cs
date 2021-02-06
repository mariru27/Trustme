using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trustme.IServices;
using Trustme.Service;
using Trustme.Models;
using Trustme.Data;

namespace Trustme.Controllers
{
    public class Test1Controller : Controller
    {
        private readonly AppContext _context;

        public Test1Controller(AppContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
