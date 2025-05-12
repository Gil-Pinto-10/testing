using Microsoft.AspNetCore.Mvc;
using VulnerableWebApi.Models;
using VulnerableWebApi.Services;
using System; // For Console.WriteLine

namespace VulnerableWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // --- SQL Injection Demo ---
        [HttpGet("sqlinjection")]
        public ActionResult<User> GetUserByUsername_SqlVulnerable([FromQuery] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be empty.");
            }
            Console.WriteLine($"[API] Received request for GetUserByUsername_SqlVulnerable with username: {username}");
            var user = _userService.RetrieveUser(username); // This calls the vulnerable DataAccess method
            if (user == null)
            {
                return NotFound($"User '{username}' not found (or SQLi mock didn't match).");
            }
            return Ok(user);
        }

        // --- NullReferenceException Demo ---
        [HttpGet("{id}/email")]
        public ActionResult<string> GetUserEmailById(int id)
        {
            var user = _userService.GetUserById(id); // Gets user by ID
            // If user is not found, 'user' will be null.
            // Then _userService.GetUserEmail(user) will throw NullReferenceException.
            // ASP.NET Core will automatically catch this and return a 500 Internal Server Error.
            if (user == null)
            {
                 // To explicitly trigger the vulnerable call with null:
                try
                {
                    Console.WriteLine($"[API] User with id {id} not found. Intentionally calling GetUserEmail with null to demonstrate NullRef.");
                    return Ok(_userService.GetUserEmail(null)); // Explicitly pass null
                }
                catch (NullReferenceException ex)
                {
                     Console.WriteLine($"[API] Caught expected NullReferenceException: {ex.Message}");
                     // Let ASP.NET Core's default error handling show a 500, or return a custom error
                     return Problem(detail: "An internal server error occurred: NullReferenceException.", statusCode: 500);
                }
            }
            return Ok(_userService.GetUserEmail(user));
        }
    }
}