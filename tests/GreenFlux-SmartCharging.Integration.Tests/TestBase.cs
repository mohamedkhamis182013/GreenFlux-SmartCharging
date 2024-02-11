using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using GreenFlux_SmartCharging.Infrastructure.Persistence;

namespace GreenFlux_SmartCharging.Integration.Tests;
public class TestBase : IDisposable
{
    protected TestDatabaseInitializer TestDb;
    protected HttpClient Client;
    protected WebApplicationFactory<Program> Factory;

    public TestBase()
    {
        TestDb = new TestDatabaseInitializer();
        Factory = new WebApplicationFactory<Program>();
        Factory = Factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");

            _ = builder.ConfigureTestServices(services =>
            {
                services.AddScoped(_ => new AppDbContext(TestDb.ContextOptions));
            });
        });
        Client = Factory.CreateClient();
    }

    public void Dispose()
    {
        TestDb.Dispose();
        Factory.Dispose();
        Client.Dispose();
    }
}
