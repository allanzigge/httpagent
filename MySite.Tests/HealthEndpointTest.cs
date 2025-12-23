using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MySite.Tests;

[TestFixture]
public class HealthEndpointTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!; // Routes requests into the app pipeline
    private List<LogEntry> _logEntries = null!;

    [SetUp]
    public void Setup()
    {
        _logEntries = new List<LogEntry>();

        // Create factory with a test logger
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Replace ILogger with a test logger
                    services.AddSingleton<ILogger<Program>>(new TestLogger(_logEntries));
                });
            });

        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task Health_Returns_200()
    {
        var response = await _client.GetAsync("/health");                // GET request
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK)); // code 200 
    }

    [Test]
    public async Task Health_Endpoint_Logs_GET_Request()
    {
        await _client.GetAsync("/health");

        Assert.That(_logEntries.Exists(e => e.Message.Contains("the /health endpoint was called")), Is.True);
    }
}

// Simple in-memory test logger
public class TestLogger : ILogger<Program>
{
    private readonly List<LogEntry> _logEntries;

    public TestLogger(List<LogEntry> logEntries)
    {
        _logEntries = logEntries;
    }

    public IDisposable BeginScope<TState>(TState state) => null!;
    public bool IsEnabled(LogLevel logLevel) => true;
    public void Log<TState>(LogLevel logLevel, EventId eventId,
        TState state, Exception exception, Func<TState, Exception?, string> formatter)
    {
        _logEntries.Add(new LogEntry
        {
            Level = logLevel,
            Message = formatter(state, exception)
        });
    }
}

public record LogEntry
{
    public LogLevel Level { get; init; }
    public string Message { get; init; } = "";
}
