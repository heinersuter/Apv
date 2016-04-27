using System.Security.Claims;

using Apv.WebApi.Users;

using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Apv.WebApi.Controllers
{
    [Route("api")]
    public class AccountController : Controller
    {
        [HttpPost("login")]
        public async void Login([FromBody]UserLogin userlogin)
        {
            var claims = new[] { new Claim("name", userlogin.UserName), new Claim(ClaimTypes.Role, "Admin") };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        [Authorize]
        [HttpPost("logout")]
        public void Logout()
        {
            HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
