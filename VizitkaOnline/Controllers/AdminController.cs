using Microsoft.AspNetCore.Mvc;
using VizitkaOnline.Logic;

namespace VizitkaOnline.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminPanel()
        {
            if (CookiesLogic.GetCookies(HttpContext) == "Admin")
            { 
                return View(DataLogic.GetAllUser());
            }
            else
            {
                return RedirectToAction("PageErrorController", "index");
            }
        }
    }
}
