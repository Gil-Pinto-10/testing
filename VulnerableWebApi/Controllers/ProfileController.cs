using Microsoft.AspNetCore.Mvc;
using VulnerableWebApi.Services; // Assuming DataAccess is here
using System;

namespace VulnerableWebApi.Controllers
{
    public class UpdateProfileRequest
    {
        public string ProfileData { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly DataAccess _dataAccess; // Directly use DataAccess here for this specific demo

        public ProfileController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // --- Empty Catch Block Demo ---
        [HttpPost("update/{userId}")]
        public IActionResult UpdateUserProfile(int userId, [FromBody] UpdateProfileRequest request)
        {
            Console.WriteLine($"[API] Received request to update profile for userId: {userId} with data: {request?.ProfileData}");
            _dataAccess.UpdateUserProfile_Vulnerable(userId, request?.ProfileData);

            // Because the error in UpdateUserProfile_Vulnerable is swallowed,
            // this endpoint will likely return 200 OK even if an internal error occurred.
            // The client will not know something went wrong unless they check server logs
            // or notice data inconsistencies later.
            // The console output from DataAccess will show "An unspecified error occurred and was silently ignored."
            return Ok(new { Message = "Profile update attempted. Check server logs for details if issues arise.", UserId = userId });
        }
    }
}