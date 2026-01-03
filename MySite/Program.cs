using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/app.log")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapGet("/health", (ILogger<Program> logger) =>
{
    
    logger.LogInformation("the /health endpoint was called");
    return "health page";
});


app.Run();

public partial class Program { } 