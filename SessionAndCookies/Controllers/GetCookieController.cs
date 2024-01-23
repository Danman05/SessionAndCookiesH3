using Microsoft.AspNetCore.Mvc;

namespace SessionAndCookies.Controllers
{

    [Route("[controller]")]
    public class GetCookieController : Controller
    {
        [HttpGet]
        public string GetPathCookie()
        {
            string? cookie = Request.Cookies["controllerPath"];
            if (string.IsNullOrEmpty(cookie))
            {
                return "Unknown";
            }
            return $"cookie found: {cookie}";
        }
    }
}
