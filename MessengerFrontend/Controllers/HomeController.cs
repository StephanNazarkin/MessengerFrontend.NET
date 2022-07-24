using MessengerFrontend.Filters;
using MessengerFrontend.Models;
using MessengerFrontend.Routes;
using MessengerFrontend.Services;
using MessengerFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessengerFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatServiceAPI _chatServiceAPI;
        private readonly IAccountServiceAPI _accountServiceAPI;

        private string Token => HttpContext.Session.GetString("Token");

        #region Constructor

        public HomeController(IChatServiceAPI chatServiceAPI, IAccountServiceAPI accountServiceAPI)
        {
            _chatServiceAPI = chatServiceAPI;
            _accountServiceAPI = accountServiceAPI;
        }

        #endregion

        #region Services

        [AuthorizationFilter]
        public async Task<IActionResult> Index()
        {
            var result = await _chatServiceAPI.GetAllChatrooms();
            ViewBag.AllChats = result;

            bool isUserAdmin = await _accountServiceAPI.IsUserSuperAdmin();

            ViewBag.Layout = "_LayoutDefault";

            if (isUserAdmin)
            {
                ViewBag.Layout = "_LayoutAdmin";
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}