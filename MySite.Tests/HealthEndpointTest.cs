using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MySite.Tests;

[TestFixture]
public class HealthEndpointTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!; // Routes requests into the app pipeline

    [SetUp]
    public void Setup()
    {
       _factory = new WebApplicationFactory<Program>();
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
}
