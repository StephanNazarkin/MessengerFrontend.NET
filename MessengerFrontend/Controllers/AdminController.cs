using MessengerFrontend.Filters;
using MessengerFrontend.Routes;
using MessengerFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessengerFrontend.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAccountServiceAPI _accountServiceAPI;
        private readonly IActionLogServiceAPI _actionLogServiceAPI;

        #region Constructor

        public AdminController(IAccountServiceAPI accountServiceAPI, 
            IActionLogServiceAPI actionLogServiceAPI)
        {
            _accountServiceAPI = accountServiceAPI;
            _actionLogServiceAPI = actionLogServiceAPI;
        }

        #endregion

        #region Methods

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            bool isUserAdmin = await _accountServiceAPI.IsUserSuperAdmin();

            if (!isUserAdmin)
            {
                return Redirect(RoutesApp.Home);
            }

            var logs = await _actionLogServiceAPI.GetAllLogs();

            ViewBag.Logs = logs;

            return View();
        }

        #endregion
    }
}
