using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VizitkaOnline.Models;

namespace VizitkaOnline.Controllers
{
    public class UserController : Controller
    {

        private ApplicationContext db { get; set; }

        public UserController(ApplicationContext applicationContext)
        {
            db = applicationContext;
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
        [HttpGet("/user/{login}")]
        public IActionResult UserVizit(string login)
        {
            return View(db.userModel.Where(u => u.Login.Contains(login)).First());
        }

        public IActionResult CheckUser(UserModel user)
        {
            if (db.userModel.Where(l => l.PasswordHash.Contains(user.PasswordHash) && l.Login.Contains(user.Login)).Count() == 0)
            {
                TempData["MessageErrorInput"] = "Введенный пароль не верный.";
                return RedirectToAction("Login", TempData);
            }
            TempData["Login"] = user.Login;
            return RedirectToAction("UserCabinet");
        }

        public IActionResult UserCabinet()
        {
            return View(db.userModel.Where(u => u.Login.Contains((string)TempData["Login"])).First());
        }


        /// <summary>
        /// Метод для создания юзера
        /// </summary>
        /// <param name="user"> UserModel аутификации</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(UserModel user)
        {
            if (user.Login == null || user.PasswordHash == null || user.Email == null || user.FirstName == null)
            {
                TempData["MessageErrorInput"] = "Ошибка ввода данных. Пустые поля";
                return RedirectToAction("Registration", TempData);
            }
            if(db.userModel.Where(l => l.Login.Contains(user.Login)).Count() > 0)
            {
                TempData["MessageErrorInput"] = "Данное имя занято, попробуйте другое";
                return RedirectToAction("Registration", TempData);
            }
            db.userModel.Add(user);
            await db.SaveChangesAsync();
            TempData["Login"] = user.Login;
            return RedirectToAction("UserCabinet");
        }
    }
}
