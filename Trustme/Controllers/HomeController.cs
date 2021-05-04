using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Trustme.IServices;
using Trustme.Models;

namespace Trustme.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _UserRepository;
        private readonly IEmailSender _EmailSender;
        private readonly IUnsignedDocumentRepository _UnsignedDocumentRepository;

        public HomeController(IUserRepository userRepository, IEmailSender emailSender, IUnsignedDocumentRepository unsignedDocumentRepository)
        {
            _EmailSender = emailSender;
            _UserRepository = userRepository;
            _UnsignedDocumentRepository = unsignedDocumentRepository;
            //var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //if (username != null)
            //{
            //    User user = _UserRepository.GetUserbyUsername(username);
            //    int countDelivered = _UnsignedDocumentRepository.CountDelivered(user);
            //    HttpContext.Session.SetInt32("countDelivered", countDelivered);
            //}
        }

        public IActionResult Index()
        {
            if (_UserRepository.AnyUser() == false)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToAction("LogIn", "Authenticate");
            }
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
