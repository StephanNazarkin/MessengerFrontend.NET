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
            await _accountServiceAPI.Login(model);
 
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

        public async Task<IActionResult> TestSettings()
        {
            return View();
        }

        public IActionResult EditProfileModal()
        {
            return View();
        }

        public IActionResult SearchModal()
        {
            return View();
        }

        public async Task<IActionResult> FriendListModal()
        {
            var result = await _accountServiceAPI.GetAllFriends();
            ViewBag.AllFriends = result;
            return View();
        }

        public async Task<IActionResult> BlackListModal()
        {
            var result = await _accountServiceAPI.GetAllBlockedUsers();
            ViewBag.AllBlockedUsers = result;
            return View();
        }

        public IActionResult ChangePasswordModal()
        {
            return View();
        }

        public async void UpdateUser(UserUpdateModel userModel)
        {
            //var result = await _accountServiceAPI.UpdateUser(userModel);
            //ViewBag.CurrentUser = result; 
        }
    }
}
