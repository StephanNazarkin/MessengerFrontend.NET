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

        #region Constructor

        public ChatController(IChatServiceAPI chatServiceAPI,
            IMessageServiceAPI messageServiceAPI,
            IAccountServiceAPI accountServiceAPI)
        {
            _chatServiceAPI = chatServiceAPI;
            _messageServiceAPI = messageServiceAPI;
            _accountServiceAPI = accountServiceAPI;
        }

        #endregion

        #region Services

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var allChats = await _chatServiceAPI.GetAllChatrooms(Token);
            var currentChat = await _chatServiceAPI.GetChatroom(id, Token);
            var currentUserAccount = await _chatServiceAPI.GetCurrentUserAccount(id, Token);
            var members = await _chatServiceAPI.GetAllMembers(id, Token);
            var messages = await _messageServiceAPI.GetMessagesFromChat(id, Token);

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

        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> CreateChat(ChatCreateModel model)
        {
            var response = await _chatServiceAPI.CreateChatroom(model, Token);

            return Redirect(string.Format(RoutesApp.Chat, response.Id));
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> GetAllMessages(int id)
        {
            var messages = await _messageServiceAPI.GetMessagesFromChat(id, Token);
            var currentUserAccount = await _chatServiceAPI.GetCurrentUserAccount(id, Token);
            ViewBag.ChatId = id;
            ViewBag.CurrentUserAccount = currentUserAccount;

            return View(messages.Reverse());
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> EditChat(int id)
        {
            var currentChat = await _chatServiceAPI.GetChatroom(id, Token);

            return View(currentChat);
        }

        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> EditChat(ChatUpdateModel model)
        {
            var response = await _chatServiceAPI.EditChatroom(model, Token);

            return Redirect(string.Format(RoutesApp.Chat, response.Id));
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> DeleteChat(int id)
        {
            var response = await _chatServiceAPI.DeleteChatroom(id, Token);

            return Redirect(RoutesApp.Home);
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> GetMembers(int id)
        {
            var response = await _chatServiceAPI.GetAllMembers(id, Token);
            var currentUserAccount = await _chatServiceAPI.GetCurrentUserAccount(id, Token);

            ViewBag.currentUserAccount = currentUserAccount;

            return View(response);
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> InviteFriend(int id)
        {
            var friends = await _accountServiceAPI.GetAllFriends(Token);
            ViewBag.ChatId = id;

            return View(friends);
        }

        [AuthorizationFilter]
        public async Task<IActionResult> AddToChatroom(ChatInviteModel model)
        {
            var response = await _chatServiceAPI.AddToChatroom(model, Token);

            return Redirect(string.Format(RoutesApp.Chat, response.ChatId));
        }

        [AuthorizationFilter]
        public async Task<IActionResult> SetAdmin(int userAccountId)
        {
            var response = await _chatServiceAPI.SetAdmin(userAccountId, Token);

            return Redirect(string.Format(RoutesApp.Chat, response.ChatId));
        }

        [AuthorizationFilter]
        public async Task<IActionResult> UnsetAdmin(int userAccountId)
        {
            var response = await _chatServiceAPI.UnsetAdmin(userAccountId, Token);

            return Redirect(string.Format(RoutesApp.Chat, response.ChatId));
        }

        [AuthorizationFilter]
        public async Task<IActionResult> MuteUser(int userAccountId)
        {
            var response = await _chatServiceAPI.MuteUser(userAccountId, Token);

            return Redirect(string.Format(RoutesApp.Chat, response.ChatId));
        }

        [AuthorizationFilter]
        public async Task<IActionResult> UnmuteUser(int userAccountId)
        {
            var response = await _chatServiceAPI.UnmuteUser(userAccountId, Token);

            return Redirect(string.Format(RoutesApp.Chat, response.ChatId));
        }

        public async Task<IActionResult> KickUser(int userAccountId)
        {
            var response = await _chatServiceAPI.KickUser(userAccountId, Token);

            return Redirect(RoutesApp.Home);
        }

        [AuthorizationFilter]
        public async Task<IActionResult> LeaveChat(int id)
        {
            var response = await _chatServiceAPI.LeaveChat(id, Token);

            return Redirect(RoutesApp.Home);
        }

        #endregion
    }
}