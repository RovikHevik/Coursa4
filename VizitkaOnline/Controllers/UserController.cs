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
using System.Text.Json;
using System.Threading.Tasks;
using VizitkaOnline.Logic;
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
        [HttpGet("/user/get/{login}")]
        public IActionResult UserVizit(string login)
        {
            return View(db.accountModel.Where(u => u.Login.Contains(login)).First());
        }

        [HttpGet("/user/api/{login}")]
        public string ApiUser(string login)
        {
            return JsonSerializer.Serialize(db.userModel.Where(u => u.Login.Contains(login)).First());
        }

        /// <summary>
        /// Метод авторизации
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult CheckUser(UserModel user)
        {
            if (db.userModel.Where(l => l.PasswordHash.Contains(user.PasswordHash) && l.Login.Contains(user.Login)).Count() == 0)
            {
                TempData["MessageErrorInput"] = "Введенный пароль не верный.";
                return RedirectToAction("Login", TempData);
            }
            AccountLogic account = new AccountLogic();
            account.SetCookies(HttpContext, user.Login);
            return RedirectToAction("UserCabinet");
        }

        public IActionResult UserCabinet()
        {
            AccountLogic account = new AccountLogic();
            string login = account.GetCookies(HttpContext);
            ViewBag.Login = login;
            return View(db.accountModel.Where(u => u.Login.Contains(login)).First());
        }


        /// <summary>
        /// Метод для создания юзера
        /// </summary>
        /// <param name="user"> UserModel аутификации</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(UserModel user)
        {
            
            if (user.Login == null || user.PasswordHash == null || user.Email == null || user.FirstName == null || user.LastName == null)
            {
                TempData["MessageErrorInput"] = "Ошибка ввода данных. Пустые поля";
                return RedirectToAction("Registration", TempData);
            }
            if(db.userModel.Where(l => l.Login.Contains(user.Login)).Count() > 0)
            {
                TempData["MessageErrorInput"] = "Данное имя занято, попробуйте другое";
                return RedirectToAction("Registration", TempData);
            }
            AccountLogic account        = new AccountLogic();
            AccountModel accountModel   = new AccountModel();
            accountModel.id             = user.id;
            accountModel.Login          = user.Login;           
            accountModel.FullName       = user.FirstName + " " + user.LastName;
            accountModel.UserPicture = "/img/default_av.jpg";
            db.accountModel.Add(accountModel);
            db.userModel.Add(user);
            await db.SaveChangesAsync();
            account.SetCookies(HttpContext, user.Login);
            return RedirectToAction("UserCabinet");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccountInfo(AccountModel model)
        {
            AccountLogic account = new AccountLogic();
            string login = account.GetCookies(HttpContext);
            ViewBag.Login = login;
            var old = db.accountModel.FirstOrDefault(ac => ac.Login.Contains(login));
            old.FullName = model.FullName;
            old.FaceBook = model.FaceBook;
            old.UserPicture = model.UserPicture;
            old.Telegram = model.Telegram;
            old.Monobank = model.Monobank;
            old.Instagram = model.Instagram;
            old.Phone = model.Phone;
            await db.SaveChangesAsync();
            return RedirectToAction("UserCabinet");
        }
    }
}
