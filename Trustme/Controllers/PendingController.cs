using Microsoft.AspNetCore.Mvc;
using Trustme.IServices;

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

            return View();
        }
    }
}
