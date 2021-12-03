using Microsoft.AspNetCore.Http;

namespace VizitkaOnline.Logic
{
    public static class CookiesLogic
    {   
        /// <summary>
        /// Метод получения кукисов
        /// </summary>
        /// <param name="key"> ключ куки </param>
        /// <param name="httpContext"> http context</param>
        /// <returns>возвращает логин</returns>
        public static string GetCookies(HttpContext httpContext, string key = "login")
        {
            return httpContext.Session.GetString(key);
        }

        /// <summary>
        /// Метод установки кукисов
        /// </summary>
        /// <param name="key">ключ установки</param>
        /// <param name="value">значение куки (логин)</param>
        /// <param name="httpContext"></param>
        public static void SetCookies(HttpContext httpContext, string value, string key = "login")
        {           
            httpContext.Session.SetString(key, value);
        }

        /// <summary>
        /// Метод удаления кукиса
        /// </summary>
        /// <param name="httpContext"> http context</param>
        /// <param name="key"> ключ куки</param>
        public static void DeleteCookies(HttpContext httpContext, string key = "login")
        {
            httpContext.Session.Remove(key);
        }
    }
}
