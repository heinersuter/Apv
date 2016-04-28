using System.Linq;
using System.Threading.Tasks;

using Apv.WebApi.Users;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

namespace Apv.WebApi.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserContext _userContext;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, UserContext userContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userContext = userContext;
            Register();
        }

        [HttpPost("login")]
        public async Task<UserLogin> Login([FromBody]UserLogin userLogin)
        {
            //var claims = new[] { new Claim("name", userLogin.UserName), new Claim(ClaimTypes.Role, "Admin") };
            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            var result = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, false, false);
            if (result.Succeeded)
            {
                return userLogin;
            }
            return new UserLogin();
        }

        [Authorize]
        [HttpPost("logout")]
        public async void Logout()
        {
            //HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async void Register()
        {
            _userContext.Database.EnsureCreated();
            if (!_userContext.Users.Any())
            {
                var user = new User { UserName = "heiner", Email = "heiner.suter@gmx.ch", MemberId = 26 };
                var result = await _userManager.CreateAsync(user, "Password_1");
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }
        }
    }
}
