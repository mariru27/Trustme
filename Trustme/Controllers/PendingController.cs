using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Trustme.IServices;
using Trustme.Models;

namespace Trustme.Controllers
{
    public class PendingController : Controller
    {
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        private readonly IPendingRepository _PendingRepository;
        private readonly IEmailSender _EmailSender;
        public PendingController(IHttpRequestFunctions httpRequestFunctions, IEmailSender emailSender, IPendingRepository pendingRepository)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            _PendingRepository = pendingRepository;
            _EmailSender = emailSender;
        }

        [HttpGet]
        public IActionResult PendingList()
        {
            User currentUser = _HttpRequestFunctions.GetUser(HttpContext);
            IEnumerable<Pending> pendingRequests = _PendingRepository.ListAllPendingRequests(currentUser);
            _PendingRepository.MarkSeen(currentUser);
            return View(pendingRequests);
        }

        [HttpGet]
        public IActionResult AllowUserSendDocuments(int IdPedingUsers)
        {
            if (IdPedingUsers != 0)
            {
                User currentUser = _HttpRequestFunctions.GetUser(HttpContext);
                Pending pending = _PendingRepository.GetPending(currentUser, IdPedingUsers);

                _PendingRepository.MarkUserAcceptPendingFromUsername(currentUser, pending.UsernameWhoSentPending);
            }
            return RedirectToAction("PendingList");
        }

        [HttpGet]
        public IActionResult BlockUserSendDocuments(int IdPedingUsers)
        {
            if (IdPedingUsers != 0)
            {
                User currentUser = _HttpRequestFunctions.GetUser(HttpContext);
                Pending pending = _PendingRepository.GetPending(currentUser, IdPedingUsers);

                _PendingRepository.Block(currentUser, pending.IdPedingUsers);
            }
            return RedirectToAction("PendingList");
        }
    }
}
