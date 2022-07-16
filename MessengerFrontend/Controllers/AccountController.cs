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
            return View();
        }

        [AuthorizationFilter]
        public IActionResult EditProfileModal()
        {
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
        public IActionResult ChangePasswordModal()
        {
            return View();
        }

        [NonAction]
        public async void GetUserByUserName(string userName)
        {
            var user = await _accountServiceAPI.GetUserByUserName(userName);
            ViewBag.FoundUser = user;
        }

        [HttpGet]
        public async Task<IActionResult> AddFriend(string userId)
        {
            var user = await _accountServiceAPI.AddFriend(userId);

            return Redirect(RoutesApp.AccountSettings);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFriend(string userId)
        {
            var user = await _accountServiceAPI.DeleteFriend(userId);

            return Redirect(RoutesApp.AccountSettings);
        }

        [HttpGet]
        public async Task<IActionResult> BlockUser(string userId)
        {
            var user = await _accountServiceAPI.BlockUser(userId);

            return Redirect(RoutesApp.AccountSettings);
        }

        [HttpGet]
        public async Task<IActionResult> UnblockUser(string userId)
        {
            var user = await _accountServiceAPI.UnblockUser(userId);

            return Redirect(RoutesApp.AccountSettings);
        }

        public async Task<IActionResult> UpdateUser(UserUpdateModel userModel)
        {
            var user = await _accountServiceAPI.UpdateUser(userModel);

            return Redirect(RoutesApp.AccountSettings);
        }

    }
}
