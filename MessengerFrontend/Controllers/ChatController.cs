using MessengerFrontend.Filters;
using MessengerFrontend.Models.Chats;
using MessengerFrontend.Routes;
using MessengerFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessengerFrontend.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatServiceAPI _chatServiceAPI;
        private readonly IMessageServiceAPI _messageServiceAPI;
        private readonly IAccountServiceAPI _accountServiceAPI;

        private string Token => HttpContext.Session.GetString("Token");

        public ChatController(IChatServiceAPI chatServiceAPI,
            IMessageServiceAPI messageServiceAPI,
            IAccountServiceAPI accountServiceAPI)
        {
            _chatServiceAPI = chatServiceAPI;
            _messageServiceAPI = messageServiceAPI;
            _accountServiceAPI = accountServiceAPI;
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var allChats = await _chatServiceAPI.GetAllChatrooms();
            var currentChat = await _chatServiceAPI.GetChatroom(id);
            var currentUserAccount = await _chatServiceAPI.GetCurrentUserAccount(id);
            var members = await _chatServiceAPI.GetAllMembers(id);
            var messages = await _messageServiceAPI.GetMessagesFromChat(id);

            ViewBag.AllChats = allChats;
            ViewBag.CurrentUserAccount = currentUserAccount;
            ViewBag.Members = members;
            ViewBag.Messages = messages.Reverse();

            return View(currentChat);
        }

        [AuthorizationFilter]
        [HttpGet]
        public IActionResult CreateChat()
        {
            return View();
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> CreateChat(ChatCreateModel model)
        {
            var response = await _chatServiceAPI.CreateChatroom(model);

            return Redirect(string.Format(RoutesApp.Chat, response.Id));
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> GetAllMessages(int id)
        {
            var messages = await _messageServiceAPI.GetMessagesFromChat(id);
            var currentUserAccount = await _chatServiceAPI.GetCurrentUserAccount(id);
            ViewBag.ChatId = id;
            ViewBag.CurrentUserAccount = currentUserAccount;

            return View(messages.Reverse());
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> EditChat(int id)
        {
            var currentChat = await _chatServiceAPI.GetChatroom(id);

            return View(currentChat);
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> EditChat(ChatUpdateModel model)
        {
            var response = await _chatServiceAPI.EditChatroom(model);

            return Redirect(string.Format(RoutesApp.Chat, response.Id));
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> DeleteChat(int id)
        {
            var response = await _chatServiceAPI.DeleteChatroom(id);

            return Redirect(RoutesApp.Home);
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> GetMembers(int id)
        {
            var response = await _chatServiceAPI.GetAllMembers(id);
            var currentUserAccount = await _chatServiceAPI.GetCurrentUserAccount(id);

            ViewBag.currentUserAccount = currentUserAccount;

            return View(response);
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> InviteFriend(int id)
        {
            var friends = await _accountServiceAPI.GetAllFriends();
            ViewBag.ChatId = id;

            return View(friends);
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        public async Task<IActionResult> AddToChatroom(ChatInviteModel model)
        {
            var response = await _chatServiceAPI.AddToChatroom(model);

            return Redirect(string.Format(RoutesApp.Chat, response.ChatId));
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        public async Task<IActionResult> SetAdmin(int userAccountId)
        {
            var response = await _chatServiceAPI.SetAdmin(userAccountId);

            return Redirect(string.Format(RoutesApp.Chat, response.ChatId));
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        public async Task<IActionResult> UnsetAdmin(int userAccountId)
        {
            var response = await _chatServiceAPI.UnsetAdmin(userAccountId);

            return Redirect(string.Format(RoutesApp.Chat, response.ChatId));
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        public async Task<IActionResult> MuteUser(int userAccountId)
        {
            var response = await _chatServiceAPI.MuteUser(userAccountId);

            return Redirect(string.Format(RoutesApp.Chat, response.ChatId));
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        public async Task<IActionResult> UnmuteUser(int userAccountId)
        {
            var response = await _chatServiceAPI.UnmuteUser(userAccountId);

            return Redirect(string.Format(RoutesApp.Chat, response.ChatId));
        }

        [MessengerExceptionHandlerFilter]
        public async Task<IActionResult> KickUser(int userAccountId)
        {
            var response = await _chatServiceAPI.KickUser(userAccountId);

            return Redirect(RoutesApp.Home);
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        public async Task<IActionResult> LeaveChat(int id)
        {
            var response = await _chatServiceAPI.LeaveChat(id);

            return Redirect(RoutesApp.Home);
        }
    }
}