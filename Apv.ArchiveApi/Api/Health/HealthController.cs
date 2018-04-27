using Microsoft.AspNetCore.Mvc;

namespace Apv.ArchiveApi.Api.Health
{
    [Route("archive/health")]
    public class HealthController : Controller
    {
        [HttpGet("check")]
        public IActionResult Check()
        {
            return Ok(new { Message = "Archive API is up and running." });
        }
    }
}
