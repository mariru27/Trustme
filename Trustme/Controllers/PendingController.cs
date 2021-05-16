using Microsoft.AspNetCore.Mvc;
using Trustme.IServices;
using Trustme.Models;

namespace Trustme.Controllers
{
    public class PendingController : Controller
    {
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        private readonly IPendingRepository _PendingRepository;
        public PendingController(IHttpRequestFunctions httpRequestFunctions, IPendingRepository pendingRepository)
        {
            _HttpRequestFunctions = httpRequestFunctions;
        }

        [HttpGet]
        public IActionResult PendingList()
        {
            User currentUser = _HttpRequestFunctions.GetUser(HttpContext);
            return View();
        }
    }
}
