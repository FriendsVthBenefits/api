using System.ComponentModel.DataAnnotations;
using api.Controllers;
using api.DTOs.Requests;
using api.DTOs.Responses;
using api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UT.Controllers;

/// <summary>
/// Defines the test class encompassing all authentication-related controller tests.
/// </summary>
public sealed class AuthenticationControllerTests
{
    #region Mocks
    /// <summary>
    /// Declares and initializes mock objects for ILogger used within authentication controller tests.
    /// </summary>
    private readonly Mock<ILogger<AuthenticationController>> logger = new();

    /// <summary>
    /// Declares and initializes mock objects for IAuthenticationService used within authentication controller tests.
    /// </summary>
    private readonly Mock<IAuthenticationService> service = new();

    /// <summary>
    /// Sets up the AuthenticationController instance with mocked dependencies for isolated testing.​
    /// </summary>
    private readonly AuthenticationController controller;
    #endregion Mocks

    /// <summary>
    /// Constructs the test controller using previously initialized mock service and logger.
    /// </summary>
    public AuthenticationControllerTests()
    {
        controller = new(logger.Object, service.Object);
    }

    #region Tests

    /// <summary>
    /// Verifies that valid credentials provided to SignIn yield an Accepted (202) response and a structured UserResponseDTO result.​
    /// </summary>
    [Test]
    public void SignIn_ValidCredentials_ReturnsAccepted()
    {
        // Arrange
        SignInRequestDTO signInRequestDTO = new()
        {
            Number = 8428558275,
            Password = "8428Ss827$"
        };

        UserResponseDTO userResponseDTO = new()
        {
            Id = 1,
            Name = "SRNP",
            Number = 8428558275,
            Mail = "naren000000000@gmail.com",
            Gender = 1,
            CreatedAt = DateTime.UtcNow,
            Location = "Asia",
            Bio = string.Empty,
            Dob = DateTime.UtcNow,
            Interests = string.Empty,
            IsActive = 1,
            LastLogin = DateTime.UtcNow,
            ProfilePictureUrl = "placeholder/placeholder/pic.jpg"
        };

        service.Setup(s => s.LoginAsync(signInRequestDTO)).ReturnsAsync(userResponseDTO);

        // Act
        var response = controller.SignIn(signInRequestDTO);

        // Assert
        Assert.Multiple(() =>
        {
            AcceptedResult result = response.Result as AcceptedResult;
            dynamic value = result!.Value!;
            var message = value.GetType().GetProperty("message")?.GetValue(value, null)?.ToString();
            var user = value.GetType().GetProperty("user")?.GetValue(value, null);
            Assert.That(result, Is.Not.Null);
            Assert.That(message, Is.EqualTo("Login successful"));
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status202Accepted));
            Assert.That(user, Is.InstanceOf<UserResponseDTO>());
        });
    }

    /// <summary>
    /// Ensures that invalid login attempts return an Unauthorized (401) response with an accurate error message.​
    /// </summary>
    [Test]
    public void SignIn_InValidCredentials_ReturnsUnAuthorized()
    {
        // Arrange
        SignInRequestDTO signInRequestDTO = new()
        {
            Number = 5428558278,
            Password = "8428$s827S"
        };

        UserResponseDTO? userResponseDTO = null;
        service.Setup(s => s.LoginAsync(signInRequestDTO)).ReturnsAsync(userResponseDTO);

        // Act
        var response = controller.SignIn(signInRequestDTO);

        // Assert
        Assert.Multiple(() =>
        {
            UnauthorizedObjectResult result = response.Result as UnauthorizedObjectResult;
            dynamic value = result!.Value!;
            var message = value.GetType().GetProperty("message")?.GetValue(value, null)?.ToString();
            Assert.That(message, Is.EqualTo("Invalid number or password"));
            Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status401Unauthorized));
        });
    }

    /// <summary>
    /// Checks that a failed validation triggers a Bad Request (400) response, confirming the controller properly validates request input.​
    /// </summary>
    [Test]
    public void SignIn_InValidCredentials_ReturnsBadRequest()
    {
        // Arrange
        SignInRequestDTO signInRequestDTO = new()
        {
            Number = 012345678,
            Password = string.Empty
        };

        var validationContext = new ValidationContext(signInRequestDTO);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(signInRequestDTO, validationContext, validationResults, true);
        foreach (var validationResult in validationResults) foreach (var memberName in validationResult.MemberNames) controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage ?? "Validation error");

        // Act
        var response = controller.SignIn(signInRequestDTO);

        // Assert
        Assert.Multiple(() =>
        {
            BadRequestObjectResult result = response.Result as BadRequestObjectResult;
            Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        });
    }
    #endregion Tests
}