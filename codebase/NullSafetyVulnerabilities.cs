// NullSafetyVulnerabilities.cs
using Microsoft.AspNetCore.Mvc; // Required for [FromServices] if you use DI for logger

namespace MyTestWebService.Vulnerabilities
{
    public static class NullSafetyVulnerabilities
    {
        // Method with a potential null reference issue (bug)
        private static string GetWelcomeMessage(string? userName, ILogger logger)
        {
            if (userName == null)
            {
                logger.LogWarning("GetWelcomeMessage received a null userName.");
                // S2259 would typically be caught on userName.Length if this check wasn't here.
                // By handling null explicitly, we avoid the crash, but the original intent
                // of showing a SAST tool detecting a direct dereference might be altered.
                // Let's keep a direct dereference example if the goal is to show that.
                // For now, let's assume we want to show a slightly safer, but still checkable version.
                return "Welcome, Guest (from modular)!";
            }

            // To still show S2259, you might have a line like:
            // if (userName.Length > 0 && userName != null) // Incorrect order for check, or just userName.Length
            
            // Let's re-introduce a clear S2259 target if name is not null but could be empty.
            // Or, more directly for the rule:
            // string somePropertyOfUserName = userName.ToUpper(); // This would be S2259 if userName could be null

            if (userName.Length > 10) // S2259: This will throw if userName is null.
            {
                return $"Welcome, {userName}, your name is quite long (from modular)!";
            }
            return $"Welcome, {userName} (from modular)!";
        }

        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/welcome-modular/{name?}", async context => {
                var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("NullSafetyVulnerabilities");
                var name = context.Request.RouteValues["name"] as string;

                try
                {
                    var message = GetWelcomeMessage(name, logger);
                    await context.Response.WriteAsync(message);
                }
                catch (NullReferenceException ex)
                {
                    logger.LogError(ex, "A null reference occurred in modular welcome.");
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An error occurred due to a null value in modular welcome.");
                }
            });
        }
    }
}
