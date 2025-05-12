using Microsoft.AspNetCore.Mvc;
using VulnerableWebApi.Services;

namespace VulnerableWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;

        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        // --- Hardcoded Secret Demo ---
        [HttpGet("check_access")]
        public IActionResult CheckAdminAccess([FromQuery] string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                return BadRequest("API key is required.");
            }

            if (_userService.CheckAdminAccess(apiKey))
            {
                return Ok("Access Granted: Welcome Admin!");
            }
            return Unauthorized("Access Denied: Invalid API Key.");
        }
    }
}