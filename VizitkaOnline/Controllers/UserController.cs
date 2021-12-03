using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VizitkaOnline.AppData;
using VizitkaOnline.Logic;
using VizitkaOnline.Models;

namespace VizitkaOnline.Controllers
{
    public class UserController : Controller
    {

        private ApplicationContext Db { get; set; }

        readonly IWebHostEnvironment _appEnvironment;

        public UserController(ApplicationContext applicationContext, IWebHostEnvironment webHostEnvironment)
        {
            Db = applicationContext;
            _appEnvironment = webHostEnvironment;
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
            AccountModel model = DataLogic.GetAccountModel(login);
            if(model == null)
            {
                return Redirect($"{HttpContext.Request.PathBase.Value}/{login}") ;
            }
            return View(model);
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

        public IActionResult LogOut()
        {
            CookiesLogic.DeleteCookies(HttpContext);
            return LocalRedirect("~/");
        }
        /// <summary>
        /// Метод для создания юзера
        /// </summary>
        /// <param name="user"> UserModel аутификации</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            
            if (DataLogic.CheckRegisterData(user) != "")
            {
                TempData["MessageErrorInput"] = DataLogic.CheckRegisterData(user);
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

        [HttpPost]
        public IActionResult AddFile(IFormFile uploadedFile)
        {
            Logic.DataLogic.SaveImage(uploadedFile, Logic.CookiesLogic.GetCookies(HttpContext), _appEnvironment);
            return RedirectToAction("UserCabinet");
        }
    }
}
