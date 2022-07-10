using MessengerFrontend.Models;
using MessengerFrontend.Services;
using MessengerFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessengerFrontend.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatServiceAPI _chatServiceAPI;

        public ChatController(IChatServiceAPI chatServiceAPI)
        {
            _chatServiceAPI = chatServiceAPI;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _chatServiceAPI.GetAllChatrooms();
            ViewBag.AllChats = result;
            return View();
        }

        public IActionResult EditChat()
        {
            return View("EditChat");
        }

        public IActionResult Members()
        {
            return View("Members");
        }

        public IActionResult InviteFriend()
        {
            return View("InviteFriend");
        }
    }
}