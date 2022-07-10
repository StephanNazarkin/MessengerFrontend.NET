using MessengerFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessengerFrontend.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServiceAPI _accountServiceAPI;

        public AccountController(IAccountServiceAPI accountServiceAPI)
        {
            _accountServiceAPI = accountServiceAPI;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ConfirmEmail()
        {
            return View();
        }

        public async Task<IActionResult> Settings()
        {
            var result = await _accountServiceAPI.GetAllFriends();
            ViewBag.AllFriends = result;
            return View();
        }
    }
}
