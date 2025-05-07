// CredentialVulnerabilities.cs
namespace MyTestWebService.Vulnerabilities
{
    public static class CredentialVulnerabilities
    {
        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/admin-secret-modular", () => {
                string adminPassword = "Password123Modular!"; // S5527: Hardcoded credentials
                return $"The modular admin password is (don't tell anyone!): {adminPassword}";
            });
        }
    }
}
