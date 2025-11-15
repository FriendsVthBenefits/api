using api.Data;
using api.DTOs.Requests;
using api.DTOs.Responses;
using api.Models;
using api.Repositories;
using api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IT;

/// <summary>
/// Contains integration tests for the authentication service operations, verifying login scenarios using in-memory database setup.
/// </summary>
[TestFixture]
public class AuthenticationServiceTests
{
    /// <summary>
    /// In-memory database context for test isolation.
    /// </summary>
    private DBContext context;

    /// <summary>
    /// Repository used to interact with authentication data in tests.
    /// </summary>
    private AuthenticationRepository repository;

    /// <summary>
    /// Authentication service instance under test.
    /// </summary>
    private AuthenticationService service;

    /// <summary>
    /// Initializes the in-memory test database and required dependencies before each test run.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DBContext>().UseInMemoryDatabase("TestDB").Options;
        context = new DBContext(options);
        context.Users.Add(new User()
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
        context.SaveChanges();
        var repositoryLogger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<AuthenticationRepository>();
        var serviceLogger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<AuthenticationService>();
        repository = new AuthenticationRepository(repositoryLogger, context);
        service = new AuthenticationService(serviceLogger, repository);
    }

    /// <summary>
    /// Cleans up database and disposes test resources after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    /// <summary>
    /// Verifies that login with valid credentials returns the expected user object.
    /// </summary>
    [Test]
    public async Task LoginAsync_WithValidCredentials_ShouldReturnUser()
    {
        // Arrange
        var request = new SignInRequestDto()
        {
            Number = 8428558275,
            Password = "8428Ss827$"
        };

        // Act
        var result = await service.LoginAsync(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<UserResponseDto>());
        Assert.That(result.Number, Is.EqualTo(request.Number));
    }

    /// <summary>
    /// Verifies that login with invalid credentials returns null.
    /// </summary>
    [Test]
    public async Task LoginAsync_WithInvalidCredentials_ShouldReturnNull()
    {
        // Arrange
        var request = new SignInRequestDto()
        {
            Number = 8428558276,
            Password = "8428S$827s"
        };

        // Act
        var result = await service.LoginAsync(request);

        // Assert
        Assert.That(result, Is.Null);
    }
}
