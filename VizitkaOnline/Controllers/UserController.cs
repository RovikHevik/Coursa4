using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VizitkaOnline.AppData;
using VizitkaOnline.Logic;
using VizitkaOnline.Models;

namespace VizitkaOnline.Controllers
{
    public class UserController : Controller
    {

        private ApplicationContext Db { get; set; }

        public UserController(ApplicationContext applicationContext)
        {
            Db = applicationContext;
        }

        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Выдаем визитку по нику
        /// </summary>
        /// <param name="login">Логин пользователя которого надо выдать</param>
        /// <returns></returns>
        [HttpGet("/user/get/{login}")]
        public IActionResult UserVizit(string login)
        {
            return View(DataLogic.GetAccountModel(login));
        }

        /// <summary>
        /// Метод авторизации
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult CheckUser(UserModel user)
        {
            if (DataLogic.LoginUser(user) != "")
            {
                TempData["MessageErrorInput"] = DataLogic.LoginUser(user);
                return RedirectToAction("Login", TempData);
            }
            CookiesLogic.SetCookies(HttpContext, user.Login);
            return RedirectToAction("UserCabinet");
        }

        public IActionResult UserCabinet()
        {
            string login = CookiesLogic.GetCookies(HttpContext);
            ViewBag.Login = login;
            return View(DataLogic.GetAccountModel(login));
        }


        /// <summary>
        /// Метод для создания юзера
        /// </summary>
        /// <param name="user"> UserModel аутификации</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            
            if (DataLogic.CheckNotNull(user) != "")
            {
                TempData["MessageErrorInput"] = DataLogic.CheckNotNull(user);
                return RedirectToAction("Registration", TempData);
            }
            DataLogic.WriteToDb(user);
            CookiesLogic.SetCookies(HttpContext, user.Login);
            return RedirectToAction("UserCabinet");
        }

        [HttpPost]
        public IActionResult UpdateAccountInfo(AccountModel model)
        {
            string login = CookiesLogic.GetCookies(HttpContext);
            ViewBag.Login = login;
            DataLogic.UpdateDB(login, model);
            return RedirectToAction("UserCabinet");
        }
    }
}
