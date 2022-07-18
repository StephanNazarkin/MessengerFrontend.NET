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

        public async Task<IActionResult> SettingsAsync()
        {
            var currentUser = await _accountServiceAPI.GetCurrentUser();
            ViewBag.CurrentUser = currentUser;

            return View();
        }

        public async Task<IActionResult> EditProfileModal()
        {
            var currentUser = await _accountServiceAPI.GetCurrentUser();
            ViewBag.CurrentUser = currentUser;

            return View();
        }

        public async Task<IActionResult> SearchModal()
        {
            var currentUser = await _accountServiceAPI.GetCurrentUser();
            ViewBag.CurrentUser = currentUser;
            var allUsers = await _accountServiceAPI.GetAllUsers();
            ViewBag.AllUsers = allUsers;

            return View();
        }

        public async Task<IActionResult> FriendListModal()
        {
            var allFriends = await _accountServiceAPI.GetAllFriends();
            ViewBag.AllFriends = allFriends;

            return View();
        }

        public async Task<IActionResult> BlackListModal()
        {
            var allBlockedUsers = await _accountServiceAPI.GetAllBlockedUsers();
            ViewBag.AllBlockedUsers = allBlockedUsers;

            return View();
        }

        public async Task<IActionResult> ChangePasswordModal()
        {
            var currentUser = await _accountServiceAPI.GetCurrentUser();
            ViewBag.CurrentUser = currentUser;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var user = await _accountServiceAPI.GetUserByUserName(userName);
            ViewBag.FoundUser = user;

            return View("FoundUser");
        }

        [HttpGet]
        public async Task<IActionResult> AddFriend(string userId)
        {
            await _accountServiceAPI.AddFriend(userId);

            return Redirect("~/Account/Settings/");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFriend(string userId)
        {
            await _accountServiceAPI.DeleteFriend(userId);

            return Redirect("~/Account/Settings/");
        }

        [HttpGet]
        public async Task<IActionResult> BlockUser(string userId)
        {
            await _accountServiceAPI.BlockUser(userId);

            return Redirect("~/Account/Settings/");
        }

        [HttpGet]
        public async Task<IActionResult> UnblockUser(string userId)
        {
            await _accountServiceAPI.UnblockUser(userId);

            return Redirect("~/Account/Settings/");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateModel userModel)
        {
            await _accountServiceAPI.UpdateUser(userModel);

            return Redirect("~/Account/Settings/");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserChangePasswordModel userModel)
        {
            _accountServiceAPI.ChangePassword(userModel);

            return Redirect("~/Account/Login/");
        }

    }
}
