using Microsoft.AspNetCore.Mvc;

namespace MessengerFrontend.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
