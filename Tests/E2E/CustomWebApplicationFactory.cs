using api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using api.Models;
using Microsoft.Extensions.Configuration;

namespace E2E;

/// <summary>
/// Custom web application factory for creating a test server instance with in-memory database and custom configuration for functional tests.
/// </summary>
/// <typeparam name="TEntryPoint">The entry point type of the ASP.NET Core application under test.</typeparam>
public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    /// <summary>
    /// Customizes the generic host creation logic for test purposes.
    /// </summary>
    /// <param name="builder">The IHostBuilder instance used to configure and build the host for the test server environment.</param>
    /// <returns>An initialized IHost containing the fully configured and constructed host instance ready for running integration tests.</returns>
    protected override IHost CreateHost(IHostBuilder builder) => base.CreateHost(builder);

    /// <summary>
    /// Configures the web host for the test environment.
    /// </summary>
    /// <param name="builder">The IWebHostBuilder used to configure the web host for the test server.</param>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.Sources.Clear();
            config.AddJsonFile("appsettings.Staging.json");
        });

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DBContext>));
            if (descriptor != null) services.Remove(descriptor);
            services.AddDbContext<DBContext>(options => options.UseInMemoryDatabase("TestDB"));
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DBContext>();
            db.Database.EnsureCreated();
            db.Users.Add(new User
            {
                Id = 1,
                Name = "SRNP",
                Number = 8428558275,
                Mail = "naren000000000@gmail.com",
                Password = "8428Ss827$",
                Gender = 1,
                Location = "Asia",
                Dob = 1762620187,
                Bio = string.Empty,
                Interests = string.Empty,
                IsActive = 1,
                Role = 1,
                Pic = new byte[1],
                CreatedAt = 1762620187,
                UpdatedAt = 1762620187,
                LastLogin = 1762620187
            });
            db.SaveChanges();
        });
    }
}

/// <summary>
/// Setup fixture for initializing and cleaning up the shared WebApplicationFactory
/// </summary>
[SetUpFixture]
public class TestAssemblySetup
{
    /// <summary>
    /// Globally shared factory for hosting the application under test.
    /// </summary>
    public static WebApplicationFactory<Program> Factory { get; private set; } = null!;

    /// <summary>
    /// Configures and starts the web host in the staging environment before tests run.
    /// </summary>
    [OneTimeSetUp]
    public static void GlobalSetup()
    {
        Factory = new CustomWebApplicationFactory<Program>().WithWebHostBuilder(builder => builder.UseEnvironment("Staging"));
    }

    /// <summary>
    /// Ensures the shared factory is disposed and resources are cleaned up after all tests have run.
    /// </summary>
    [OneTimeTearDown]
    public static void GlobalTeardown()
    {
        Factory.Dispose();
    }
}
