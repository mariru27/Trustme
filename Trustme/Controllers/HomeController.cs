using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Trustme.IServices;
using Trustme.Models;

namespace Trustme.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private IKeyRepository _KeyRepository;
        private IUserRepository _UserRepository;
        public HomeController(ILogger<HomeController> logger, IKeyRepository keyRepository, IUserRepository userRepository)
        {
            _UserRepository = userRepository;
            _logger = logger;
            _KeyRepository = keyRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {

            await HttpContext.SignOutAsync();

            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
