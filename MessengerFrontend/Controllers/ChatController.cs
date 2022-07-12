using MessengerFrontend.Models;
using MessengerFrontend.Models.Chats;
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
            var currentUserAccount = await _chatServiceAPI.GetCurrentUserAccount(id);
            var members = await _chatServiceAPI.GetAllMembers(id);

            ViewBag.AllChats = allChats;
            ViewBag.CurrentUserAccount = currentUserAccount;
            ViewBag.Members = members;

            return View(currentChat);
        }

        [HttpGet]
        public IActionResult CreateChat()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(ChatCreateModel model)
        {
            var response = await _chatServiceAPI.CreateChatroom(model);

            return Redirect("~/Chat/Index/" + response.Id);
        }

        public IActionResult EditChat()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers(int id)
        {
            var response = await _chatServiceAPI.GetAllMembers(id);
            var currentUserAccount = await _chatServiceAPI.GetCurrentUserAccount(id);

            ViewBag.currentUserAccount = currentUserAccount;

            return View(response);
        }
        
        public async Task<IActionResult> SetAdmin(int userAccountId)
        {
            var response = await _chatServiceAPI.SetAdmin(userAccountId);

            return Redirect("~/Chat/Index/" + response.ChatId);
        }

        public async Task<IActionResult> UnsetAdmin(int userAccountId)
        {
            var response = await _chatServiceAPI.UnsetAdmin(userAccountId);

            return Redirect("~/Chat/Index/" + response.ChatId);
        }

        public async Task<IActionResult> MuteUser(int userAccountId)
        {
            var response = await _chatServiceAPI.MuteUser(userAccountId);

            return Redirect("~/Chat/Index/" + response.ChatId);
        }

        public async Task<IActionResult> UnmuteUser(int userAccountId)
        {
            var response = await _chatServiceAPI.UnmuteUser(userAccountId);

            return Redirect("~/Chat/Index/" + response.ChatId);
        }

        public async Task<IActionResult> KickUser(int userAccountId)
        {
            var response = await _chatServiceAPI.KickUser(userAccountId);

            return Redirect("~/");
        }

        public IActionResult InviteFriend()
        {
            return View();
        }

        public async Task<IActionResult> LeaveChat(int id)
        {
            var response = await _chatServiceAPI.LeaveChat(id);

            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageCreateModel model)
        {
            bool response = await _messageServiceAPI.SendMessage(model);

            return Redirect("~/Chat/Index/" + model.ChatId);
        }
    }
}