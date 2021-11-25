using Microsoft.AspNetCore.Mvc;
using System;
using VizitkaOnline.Logic;
using VizitkaOnline.Models;

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
            SendLogIntoTelegram logger = new();
            ErrorTelegramModel model = new();
            model.Login = CookiesLogic.GetCookies(HttpContext);
            model.UserAgent = Request.Headers["User-Agent"].ToString();
            model.PageUrl = page;
            model.DateTimeOfError = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            logger.SendLog(model);

            ViewBag.returnUrl = page;
            return View();
        }
    }
}
