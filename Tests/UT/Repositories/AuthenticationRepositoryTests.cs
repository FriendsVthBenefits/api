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
    /// Sets up the AuthenticationRepository instance with mocked dependencies for isolated testing.​
    /// </summary>
    private readonly AuthenticationRepository repository;
    #endregion Mocks

    /// <summary>
    /// Constructs the test service using previously initialized mock repository and logger.
    /// </summary>
    public AuthenticationServiceTests()
    {
        var options = new DbContextOptionsBuilder<YourDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
        using var context = new YourDbContext(options);
        /* context.Users.AddRange(
            new User()
            {
                Id = 0,
                Name = "SRNP",
                Number = 8428558275,
                Mail = "naren000000000@gmail.com",
                Gender = 1,
                Dob = 1741710312,
                Location = "Asia",
                ProfilePictureUrl = null,
                Bio = null,
                Interests = null,
                LastLogin = 1741710312,
                CreatedAt = 1741710312,
                IsActive = 1
            }
        ); */
        context.SaveChanges();
        repository = new(logger.Object, context);
    }
    
    #region Tests
    /// <summary>
    /// Verifies that invalid credentials provided to user exist async yield an null response.​
    /// </summary>
    [Test]
    public void UserExistAsync_InValidCredentials_ReturnsNull()
    {
        // Arrange
        SignInRequestDTO signInRequestDTO = new()
        {
            Number = 8428558270,
            Password = "8428$s827S"
        };

        User? user = null;
        context.Setup(s => s.UserExistAsync(signInRequestDTO)).ReturnsAsync(user);

        // Act
        var response = repository.LoginAsync(signInRequestDTO);

        // Assert
        Assert.That(response, Is.Null);
    }
    #endregion Tests
}