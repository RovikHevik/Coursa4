using Microsoft.AspNetCore.Mvc;

namespace VizitkaOnline.Controllers
{
    public class PageErrorController : Controller
    {
        /// <summary>
        /// 404 error conroller
        /// </summary>
        /// <returns></returns>
        [HttpGet("/{page}")]
        public IActionResult Index(string page)
        {
            ViewBag.returnUrl = page;
            return View();
        }
    }
}
