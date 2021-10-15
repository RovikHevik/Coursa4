using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
