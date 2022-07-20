using Microsoft.AspNetCore.Mvc;

namespace MessengerFrontend.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult Exception(string actionName, string errorMessage)
        {
            ViewBag.ActionName = actionName;
            ViewBag.Error = errorMessage;
            return View();
        }
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
