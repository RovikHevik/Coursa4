using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult UserCabinet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(List<string> regData)
        {
            return View("UserCabinet");
        }
    }
}
