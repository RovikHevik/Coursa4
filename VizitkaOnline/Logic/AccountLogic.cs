using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VizitkaOnline.Models;

namespace VizitkaOnline.Logic
{
    public class AccountLogic : Controller, IAccountLogic, ICookies
    {   

        public UserModel GetUserModel(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateDB(int id, UserModel userModel)
        {
            throw new System.NotImplementedException();
        }

        public void WriteToDb(UserModel userModel)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Метод получения кукисов
        /// </summary>
        /// <param name="key"> ключ куки </param>
        /// <param name="httpContext"> http context</param>
        /// <returns>возвращает логин</returns>
        public string GetCookies(HttpContext httpContext, string key = "login")
        {
            return httpContext.Session.GetString(key);
        }

        /// <summary>
        /// Метод установки кукисов
        /// </summary>
        /// <param name="key">ключ установки</param>
        /// <param name="value">значение куки (логин)</param>
        /// <param name="httpContext"></param>
        public void SetCookies(HttpContext httpContext, string value, string key = "login")
        {           
            httpContext.Session.SetString(key, value);
        }
    }
}
