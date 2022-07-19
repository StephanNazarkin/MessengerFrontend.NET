using MessengerFrontend.Filters;
using MessengerFrontend.Models.Messages;
using MessengerFrontend.Routes;
using MessengerFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessengerFrontend.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageServiceAPI _messageServiceAPI;

        public MessageController(IMessageServiceAPI messageServiceAPI)
        {
            _messageServiceAPI = messageServiceAPI;
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> Index(int id, int chatId)
        {
            var model = await _messageServiceAPI.GetMessage(id);
            ViewBag.ChatId = chatId;

            return View(model);
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageCreateModel model)
        {
            bool response = await _messageServiceAPI.SendMessage(model);

            return Redirect(string.Format(RoutesApp.Chat, model.ChatId));
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> Edit(MessageUpdateModel model)
        {
            var response = await _messageServiceAPI.EditMessage(model);

            return Redirect(string.Format(RoutesApp.Chat, model.ChatId));
        }

        [MessengerExceptionHandlerFilter]
        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> Delete(int id, int chatId)
        {
            var response = await _messageServiceAPI.DeleteMessage(id);

            return Redirect(string.Format(RoutesApp.Chat, chatId));
        }
    }
}
