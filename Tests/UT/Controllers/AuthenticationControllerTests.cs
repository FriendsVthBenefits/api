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
    private readonly Mock<IAuthenticationService> service = new(MockBehavior.Strict);

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
    [Test, Order(2)]
    public async Task SignInAsync_ValidCredentials_ReturnsAccepted()
    {
        // Arrange
        SignInRequestDto signInRequestDTO = new()
        {
            Number = 8428558275,
            Password = "8428Ss827$"
        };

        UserResponseDto userResponseDTO = new()
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
        var response = await controller.SignInAsync(signInRequestDTO) as AcceptedResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            dynamic value = response!.Value!;
            var message = value.GetType().GetProperty("message")?.GetValue(value, null)?.ToString();
            var user = value.GetType().GetProperty("user")?.GetValue(value, null);
            Assert.That(response, Is.Not.Null);
            Assert.That(message, Is.EqualTo("Login successful"));
            Assert.That(response.StatusCode, Is.EqualTo(StatusCodes.Status202Accepted));
            Assert.That(user, Is.InstanceOf<UserResponseDto>());
        });
    }

    /// <summary>
    /// Ensures that invalid login attempts return an Unauthorized (401) response with an accurate error message.​
    /// </summary>
    [Test, Order(1)]
    public async Task SignInAsync_InValidCredentials_ReturnsUnAuthorized()
    {
        // Arrange
        SignInRequestDto signInRequestDTO = new()
        {
            Number = 9876543210,
            Password = "Test@1234"
        };

        UserResponseDto? userResponseDTO = null;
        service.Setup(s => s.LoginAsync(signInRequestDTO)).ReturnsAsync(userResponseDTO);

        // Act
        var response = await controller.SignInAsync(signInRequestDTO) as UnauthorizedObjectResult;
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            dynamic value = response!.Value!;
            var message = value.GetType().GetProperty("message")?.GetValue(value, null)?.ToString();
            Assert.That(message, Is.EqualTo("Invalid number or password"));
            Assert.That(response!.StatusCode, Is.EqualTo(StatusCodes.Status401Unauthorized));
        });
    }

    /// <summary>
    /// Checks that a failed validation triggers a Bad Request (400) response, confirming the controller properly validates request input.​
    /// </summary>
    [Test, Order(3)]
    public async Task SignInAsync_InValidCredentials_ReturnsBadRequest()
    {
        // Arrange
        SignInRequestDto signInRequestDTO = new()
        {
            Number = 12345,
            Password = "password"
        };

        var validationContext = new ValidationContext(signInRequestDTO);
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(signInRequestDTO, validationContext, validationResults, true);
        foreach (var validationResult in validationResults) foreach (var memberName in validationResult.MemberNames) controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage ?? "Validation error");

        // Act
        var response = await controller.SignInAsync(signInRequestDTO) as BadRequestObjectResult;

        // Assert
        Assert.That(response!.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
    }
    #endregion Tests
}