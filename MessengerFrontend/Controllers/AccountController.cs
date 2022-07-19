using MessengerFrontend.Filters;
using MessengerFrontend.Models.Users;
using MessengerFrontend.Routes;
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
        public async Task<IActionResult> TryLogin(UserLoginModel model)
        {
            UserViewModel loggedUser = await _accountServiceAPI.Login(model);
            HttpContext.Session.SetString("Token", loggedUser.Token);

            return Redirect(RoutesApp.Home);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TryRegister(UserViewModel model)
        {
            await _accountServiceAPI.Register(model);

            return Redirect(RoutesApp.ConfirmEmail);
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

        [AuthorizationFilter]
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

        [AuthorizationFilter]
        public async Task<IActionResult> FriendListModal()
        {
            var allFriends = await _accountServiceAPI.GetAllFriends();
            ViewBag.AllFriends = allFriends;

            return View();
        }

        [AuthorizationFilter]
        public async Task<IActionResult> BlackListModal()
        {
            var allBlockedUsers = await _accountServiceAPI.GetAllBlockedUsers();
            ViewBag.AllBlockedUsers = allBlockedUsers;

            return View();
        }

        [AuthorizationFilter]
        public async Task<IActionResult> ChangePasswordModal()
        {
            var currentUser = await _accountServiceAPI.GetCurrentUser();
            ViewBag.CurrentUser = currentUser;

            return View();
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> AddFriend(string userId)
        {
            await _accountServiceAPI.AddFriend(userId);

            return Redirect(RoutesApp.AccountSettings);
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> DeleteFriend(string userId)
        {
            await _accountServiceAPI.DeleteFriend(userId);

            return Redirect(RoutesApp.AccountSettings);
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> BlockUser(string userId)
        {
            await _accountServiceAPI.BlockUser(userId);

            return Redirect(RoutesApp.AccountSettings);
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> UnblockUser(string userId)
        {
            await _accountServiceAPI.UnblockUser(userId);

            return Redirect(RoutesApp.AccountSettings);
        }

        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateModel userModel)
        {
            await _accountServiceAPI.UpdateUser(userModel);

            return Redirect(RoutesApp.AccountSettings);
        }

        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserChangePasswordModel userModel)
        {
            _accountServiceAPI.ChangePassword(userModel);

            return Redirect(RoutesApp.Login);
        }

    }
}
