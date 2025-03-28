using Day_3_ASP_.NET.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddLogging();
app.UseMiddleware<LoggingMiddleware>();
app.MapGet("/", () => "Write log into log.txt file in the root directory of the project.");
app.Run();
