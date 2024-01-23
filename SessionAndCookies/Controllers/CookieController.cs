using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SessionAndCookies.Controllers
{
    [Route("api/[controller]")]
    public class CookieController : Controller
    { 
        [HttpGet] 
        public string GetFavouriteMilkshake(string favouriteMilkshake)
        {
            if (string.IsNullOrEmpty(favouriteMilkshake))
                return "favouriteMilkshake not set";

            CookieOptions co = new CookieOptions()
            { 
                Expires = DateTime.Now.AddMinutes(5) 
            };

            Response.Cookies.Append("favouriteMilkshake", favouriteMilkshake, co);

            return $"Favourite Milkshake: {favouriteMilkshake} ";
        }


        [HttpGet]
        [Route("[action]")]
        public string GetByCookie()
        {
            string? cookie = Request.Cookies["favouriteMilkshake"];

            if (string.IsNullOrEmpty(cookie))
                return "Unknown";

            return $"[Cookie] Favourite Milkshake: {Request.Cookies["favouriteMilkshake"]}";
        }

        [HttpGet]
        [Route("[action]")]
        public string CleanCookie(string cookieName)
        {
            if (string.IsNullOrEmpty(cookieName))
                return "Need cookie name";


            string? cookie = Request.Cookies[cookieName];
            if (string.IsNullOrEmpty(cookie))
                return "Unknown";

            CookieOptions cookieOptions = new CookieOptions() { Expires =  DateTime.Now};

            Response.Cookies.Append(cookieName, cookie, cookieOptions);
            return $"Cleared cookie: Name {cookieName} Content {Request.Cookies[cookieName]}";
        }
    }
}
