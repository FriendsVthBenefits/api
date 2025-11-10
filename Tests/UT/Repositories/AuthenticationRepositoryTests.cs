using api.Data;
using api.DTOs.Requests;
using api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace UT.Repositories;

/// <summary>
/// Defines the test class encompassing all authentication-related repository tests.
/// </summary>
public sealed class AuthenticationRepositoryTests
{
    #region Mocks
    /// <summary>
    /// Declares and initializes mock objects for ILogger used within authentication repository tests.
    /// </summary>
    private readonly Mock<ILogger<AuthenticationRepository>> logger = new();

    /// <summary>
    /// DbContext instance configured for in-memory testing.
    /// </summary>
    private readonly DBContext context;

    /// <summary>
    /// Sets up the AuthenticationRepository instance with mocked dependencies for isolated testing.​
    /// </summary>
    private readonly AuthenticationRepository repository;
    #endregion Mocks

    /// <summary>
    /// Constructs the test service using previously initialized mock repository and logger.
    /// </summary>
    public AuthenticationRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DBContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;
        context = new DBContext(options);
        repository = new(logger.Object, context);
    }

    #region Tests
    /// <summary>
    /// Verifies that invalid credentials provided to user exist async yield an null response.​
    /// </summary>
    [Test]
    public async Task UserExistAsync_InValidCredentials_ReturnsNull()
    {
        // Arrange
        SignInRequestDTO signInRequestDTO = new()
        {
            Number = 8428558270,
            Password = "8428$s827S"
        };

        // Act
        var response = await repository.UserExistAsync(signInRequestDTO);

        // Assert
        Assert.That(response, Is.Null);
    }
    #endregion Tests

    /// <summary>
    /// Cleanup method to dispose context after tests.
    /// </summary>
    [OneTimeTearDown]
    public async Task CleanUp() => await context.DisposeAsync();
}