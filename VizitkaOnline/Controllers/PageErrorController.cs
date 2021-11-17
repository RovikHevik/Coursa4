using Microsoft.AspNetCore.Mvc;

namespace VizitkaOnline.Controllers
{
    public class PageErrorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View();
        }
    }
}
