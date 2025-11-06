namespace UT.Services;

/// <summary>
/// Defines the test class encompassing all authentication-related service tests.
/// </summary>
public sealed class AuthenticationServiceTests
{
    #region Mocks
    /// <summary>
    /// Declares and initializes mock objects for ILogger used within authentication service tests.
    /// </summary>
    private readonly Mock<ILogger<AuthenticationService>> logger = new();
    
    /// <summary>
    /// Declares and initializes mock objects for IAuthenticationRepository used within authentication service tests.
    /// </summary>
    private readonly Mock<IAuthenticationRepository> repository = new();

    /// <summary>
    /// Sets up the AuthenticationService instance with mocked dependencies for isolated testing.​
    /// </summary>
    private readonly AuthenticationService service;
    #endregion Mocks

    /// <summary>
    /// Constructs the test service using previously initialized mock repository and logger.
    /// </summary>
    public AuthenticationServiceTests()
    {
        service = new(logger.Object, repository.Object);
    }
    
    #region Tests
    /// <summary>
    /// Verifies that invalid credentials provided to Login yield an null response.​
    /// </summary>
    [Test]
    public void Login_InValidCredentials_ReturnsNull()
    {
        // Arrange
        SignInRequestDTO signInRequestDTO = new()
        {
            Number = 8428558270,
            Password = "8428$s827S"
        };

        User? user = null;
        repository.Setup(s => s.UserExistAsync(signInRequestDTO)).ReturnsAsync(user);

        // Act
        var response = service.LoginAsync(signInRequestDTO);

        // Assert
        Assert.That(response, Is.Null);
    }

    /// <summary>
    /// Verifies that valid credentials provided to Login yield an user dto response.​
    /// </summary>
    [Test]
    public void Login_ValidCredentials_ReturnsUserResponseDTO()
    {
        // Arrange
        SignInRequestDTO signInRequestDTO = new()
        {
            Number = 8428558270,
            Password = "8428$s827S"
        };

        User user = new()
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
        };
        repository.Setup(s => s.UserExistAsync(signInRequestDTO)).ReturnsAsync(user);

        // Act
        var response = service.LoginAsync(signInRequestDTO);

        // Assert
        Assert.Multiple(() => {
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.InstanceOf<UserResponseDTO>());
            Assert.That(response.Number, Is.EqualsTo(user.Number));
        });
    }
    #endregion Tests
}