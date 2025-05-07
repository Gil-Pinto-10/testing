// Program.cs
using MyTestWebService.Vulnerabilities; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// You can add services here if needed by your vulnerability handlers, e.g., for logging
builder.Services.AddLogging();

var app = builder.Build();

// A simple base endpoint
app.MapGet("/", () => "Hello from MyTestWebService (Modular Main)!");

// Map endpoints from the new modular classes
CredentialVulnerabilities.MapEndpoints(app);
CodeSmellExamples.MapEndpoints(app);
NullSafetyVulnerabilities.MapEndpoints(app);

// The EmptyModularUtilityClass is just defined, not directly mapped as an endpoint here.
// Its presence is enough for SAST to analyze it.

app.Run();
