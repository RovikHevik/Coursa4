using Microsoft.AspNetCore.Mvc;

namespace VizitkaOnline.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult UserCabinet()
        {
            return View();
        }
    }
}
