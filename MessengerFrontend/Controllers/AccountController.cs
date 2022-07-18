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
        private string Token => HttpContext.Session.GetString("Token");

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

        [AuthorizationFilter]
        public async Task<IActionResult> SearchModal()
        {
            var currentUser = await _accountServiceAPI.GetCurrentUser(Token);
            ViewBag.CurrentUser = currentUser;
            var allUsers = await _accountServiceAPI.GetAllUsers(Token);
            ViewBag.AllUsers = allUsers;

            return View();
        }

        [AuthorizationFilter]
        public async Task<IActionResult> FriendListModal()
        {
            var allFriends = await _accountServiceAPI.GetAllFriends(Token);
            ViewBag.AllFriends = allFriends;

            return View();
        }

        [AuthorizationFilter]
        public async Task<IActionResult> BlackListModal()
        {
            var allBlockedUsers = await _accountServiceAPI.GetAllBlockedUsers(Token);
            ViewBag.AllBlockedUsers = allBlockedUsers;

            return View();
        }

        [AuthorizationFilter]
        public async IActionResult ChangePasswordModal()
        {
            var currentUser = await _accountServiceAPI.GetCurrentUser();
            ViewBag.CurrentUser = currentUser;

            return View();
        }


        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> AddFriend(string userId)
        {
            await _accountServiceAPI.AddFriend(userId, Token);

            return Redirect(RoutesApp.AccountSettings);
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> DeleteFriend(string userId)
        {
            await _accountServiceAPI.DeleteFriend(userId, Token);

            return Redirect(RoutesApp.AccountSettings);
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> BlockUser(string userId)
        {
            await _accountServiceAPI.BlockUser(userId, Token);

            return Redirect(RoutesApp.AccountSettings);
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> UnblockUser(string userId)
        {
            await _accountServiceAPI.UnblockUser(userId, Token);

            return Redirect(RoutesApp.AccountSettings);
        }

        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateModel userModel)
        {
            await _accountServiceAPI.UpdateUser(userModel, Token);

            return Redirect(RoutesApp.AccountSettings);
        }

        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserChangePasswordModel userModel)
        {
            _accountServiceAPI.ChangePassword(userModel);

            return Redirect("~/Account/Login/");
        }

    }
}
