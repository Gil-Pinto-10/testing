using Microsoft.AspNetCore.Mvc;
using VulnerableWebApi.Models;

namespace VulnerableWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private static string hardcodedApiKey = "ABC123"; // Hardcoded secret

        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpGet("get")]
        public ActionResult<User?> GetUser(string username)
        {
            return _service.GetByUsernameUnsafe(username);
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            _service.LogCredentials(username, password);
            return Ok();
        }

        [HttpPost("hash")]
        public ActionResult<string> Hash(string input)
        {
            return _service.WeakHash(input);
        }

        [HttpPost("crash")]
        public IActionResult Crash()
        {
            _service.SwallowException();
            return Ok("Maybe something went wrong...");
        }
    }
}
