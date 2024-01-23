using Microsoft.AspNetCore.Mvc;

namespace SessionAndCookies.Controllers
{
    [Route("[controller]")]
    public class PathCookieController : Controller
    {
        [HttpGet]
        public string PathCookie()
        {
            string? controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            if (string.IsNullOrEmpty(controllerName))
            {
                return "Unknown";
            }
            CookieOptions co = new CookieOptions() {
                Path = "/" + controllerName,
                Expires = DateTime.UtcNow.AddMinutes(5),
            };
            Response.Cookies.Append("controllerPath", "someValue", co);
            return "path cookie succesfull";
        }
    }
}
