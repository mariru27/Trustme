﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            _PendingRepository = pendingRepository;
        }

        [HttpGet]
        public IActionResult PendingList()
        {
            User currentUser = _HttpRequestFunctions.GetUser(HttpContext);
            IEnumerable<Pending> pendingRequests = _PendingRepository.ListAllPendingRequests(currentUser);
            return View(pendingRequests);
        }

        [HttpPost]
        public IActionResult AllowUserSendDocuments(int IdPedingUsers)
        {
            User currentUser = _HttpRequestFunctions.GetUser(HttpContext);

            return View();
        }
    }
}
