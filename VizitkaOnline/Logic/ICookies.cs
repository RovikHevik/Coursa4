using Microsoft.AspNetCore.Http;

namespace VizitkaOnline.Logic
{
    public interface ICookies
    {
        public void SetCookies(HttpContext httpContext, string value, string key);
        public string GetCookies(HttpContext httpContext, string key);
    }
}
