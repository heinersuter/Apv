using Apv.ArchiveApi.GoogleDrive;
using Microsoft.AspNetCore.Mvc;

namespace Apv.ArchiveApi.Api
{
    [Route("archive")]
    public class ArchiveController : Controller
    {
        private readonly GoogleDriveService _googleDriveService;

        public ArchiveController(GoogleDriveService googleDriveService)
        {
            _googleDriveService = googleDriveService;
        }

        [HttpGet("directories")]
        public IActionResult GetDirectories()
        {
            var directories = _googleDriveService.GetAllDirectories();

            if (directories == null)
            {
                return NotFound();

            }

            return Ok(directories);
        }
    }
}
