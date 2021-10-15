using Microsoft.AspNetCore.Mvc;

namespace LearnASPMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
