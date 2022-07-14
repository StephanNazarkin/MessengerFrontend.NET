using MessengerFrontend.Filters;
using MessengerFrontend.Models;
using MessengerFrontend.Services;
using MessengerFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessengerFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatServiceAPI _chatServiceAPI;

        public HomeController(IChatServiceAPI chatServiceAPI)
        {
            _chatServiceAPI = chatServiceAPI;
        }

        [AuthorizationFilter]
        public async Task<IActionResult> Index()
        {
            var result = await _chatServiceAPI.GetAllChatrooms();
            ViewBag.AllChats = result;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}