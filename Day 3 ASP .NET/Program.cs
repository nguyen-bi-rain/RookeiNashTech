using Day_3_ASP_.NET.Interface;
using Day_3_ASP_.NET.Middleware;
using Day_3_ASP_.NET.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICarService, CarService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();
app.Run();
