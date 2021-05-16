using Microsoft.AspNetCore.Mvc;
using Trustme.IServices;

namespace Trustme.Controllers
{
    public class PendingController : Controller
    {
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        public PendingController(IHttpRequestFunctions httpRequestFunctions)
        {
            _HttpRequestFunctions = httpRequestFunctions;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
