using api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using api.Models;
using Microsoft.Extensions.Configuration;

namespace E2E;

public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    protected override IHost CreateHost(IHostBuilder builder) => base.CreateHost(builder);

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
            services.AddDbContext<DBContext>(options => options.UseInMemoryDatabase("TestDb"));
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

[SetUpFixture]
public class TestAssemblySetup
{
    public static WebApplicationFactory<Program> Factory { get; private set; } = null!;

    [OneTimeSetUp]
    public static void GlobalSetup()
    {
        Factory = new CustomWebApplicationFactory<Program>().WithWebHostBuilder(builder => builder.UseEnvironment("Staging"));
    }

    [OneTimeTearDown]
    public static void GlobalTeardown()
    {
        Factory.Dispose();
    }
}
