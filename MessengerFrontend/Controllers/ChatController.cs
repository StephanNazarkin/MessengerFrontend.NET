using MessengerFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessengerFrontend.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}