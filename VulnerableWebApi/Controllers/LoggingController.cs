using Microsoft.AspNetCore.Mvc;
using VulnerableWebApi.Services;
using System;

namespace VulnerableWebApi.Controllers
{
    public class LogMessageRequest
    {
        public string Message { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly UserService _userService; // UserService has the vulnerable logger

        public LoggingController(UserService userService)
        {
            _userService = userService;
        }

        // --- Resource Leak Demo ---
        [HttpPost("log_activity")]
        public IActionResult LogActivity([FromBody] LogMessageRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Message))
            {
                return BadRequest("Message is required.");
            }
            Console.WriteLine($"[API] Received request to log activity: {request.Message}");
            _userService.LogActivity_Vulnerable(request.Message); // Calls the method with the resource leak
            return Ok($"Activity logged: '{request.Message}'. (Server-side resource leak may have occurred).");
        }
    }
}