using Microsoft.AspNetCore.Mvc;

namespace Apv.LoginApi.Health
{
    [Route("login/health")]
    public class HealthController : Controller
    {
        [HttpGet("check")]
        public IActionResult Check()
        {
            return Ok(new { Message = "Login API is up and running." });
        }
    }
}
