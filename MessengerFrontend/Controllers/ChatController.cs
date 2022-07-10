using MessengerFrontend.Models;
using MessengerFrontend.Models.Messages;
using MessengerFrontend.Services;
using MessengerFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessengerFrontend.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatServiceAPI _chatServiceAPI;
        private readonly IMessageServiceAPI _messageServiceAPI;
        private readonly IAccountServiceAPI _accountServiceAPI;

        public ChatController(IChatServiceAPI chatServiceAPI,
            IMessageServiceAPI messageServiceAPI,
            IAccountServiceAPI accountServiceAPI)
        {
            _chatServiceAPI = chatServiceAPI;
            _messageServiceAPI = messageServiceAPI;
            _accountServiceAPI = accountServiceAPI;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var allChats = await _chatServiceAPI.GetAllChatrooms();
            var currentChat = await _chatServiceAPI.GetChatroom(id);
            var currentUser = await _accountServiceAPI.GetCurrentUser();
            ViewBag.AllChats = allChats;
            ViewBag.CurrentUser = currentUser;

            return View(currentChat);
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

        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageCreateModel model)
        {
            await _messageServiceAPI.SendMessage(model);

            return Redirect("~/Chat/Index/" + model.ChatId);
        }
    }
}