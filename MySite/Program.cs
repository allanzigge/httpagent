var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Logger.LogInformation("app has started");

app.MapGet("/", () => "Hello World!");

app.MapGet("/health", (ILogger<Program> logger) =>
{
    logger.LogInformation("the /health endpoint was called");
    return "health page";
});


app.Run();
