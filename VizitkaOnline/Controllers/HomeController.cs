﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VizitkaOnline.Logic;
using VizitkaOnline.Models;

namespace VizitkaOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            AccountLogic logic = new AccountLogic();
            ViewBag.Login = logic.GetCookies(HttpContext);
            return View();
        }

        public IActionResult AboutUs()
        {
            AccountLogic logic = new AccountLogic();
            ViewBag.Login = logic.GetCookies(HttpContext);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            AccountLogic logic = new AccountLogic();
            ViewBag.Login = logic.GetCookies(HttpContext);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
