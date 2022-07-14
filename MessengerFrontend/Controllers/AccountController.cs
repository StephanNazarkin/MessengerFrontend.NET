using MessengerFrontend.Filters;
using MessengerFrontend.Models.Users;
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

        [HttpPost]
        public async Task<IActionResult> TryLogin(UserViewModel model)
        {
            UserViewModel loggedUser = await _accountServiceAPI.Login(model);
            HttpContext.Session.SetString("Token", loggedUser.Token);

            return Redirect("~/Chat/Index/");
        }
        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TryRegister(UserViewModel model)
        {
            await _accountServiceAPI.Register(model);

            return Redirect("~/Account/ConfirmEmail/");
        }

        public IActionResult ConfirmEmail()
        {
            return View();
        }

        [AuthorizationFilter]
        public async Task<IActionResult> TestSettings()
        {
            return View();
        }

        [AuthorizationFilter]
        public IActionResult EditProfileModal()
        {
            return View();
        }

        [AuthorizationFilter]
        public IActionResult SearchModal()
        {
            return View();
        }

        [AuthorizationFilter]
        public async Task<IActionResult> FriendListModal()
        {
            var result = await _accountServiceAPI.GetAllFriends();
            ViewBag.AllFriends = result;
            return View();
        }

        [AuthorizationFilter]
        public async Task<IActionResult> BlackListModal()
        {
            var result = await _accountServiceAPI.GetAllBlockedUsers();
            ViewBag.AllBlockedUsers = result;
            return View();
        }

        [AuthorizationFilter]
        public IActionResult ChangePasswordModal()
        {
            return View();
        }

        [AuthorizationFilter]
        public async void UpdateUser(UserUpdateModel userModel)
        {
            //var result = await _accountServiceAPI.UpdateUser(userModel);
            //ViewBag.CurrentUser = result; 
        }
    }
}
