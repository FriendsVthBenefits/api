using api.DTOs.Requests;
using api.DTOs.Responses;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

/// <summary>
/// Controller that manages user authentication operations.
/// </summary>
/// <param name="logger">Logger for logging diagnostic information.</param>
/// <param name="service">Authentication service for handling login logic.</param>
[ApiController]
[Route("[controller]")]
public class AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService service) : ControllerBase
{
    /// <summary>
    /// Logger instance for recording logs through the controller lifecycle.
    /// </summary>
    private readonly ILogger<AuthenticationController> _logger = logger;

    /// <summary>
    /// Instance of the authentication service for business logic related to authentication.
    /// </summary>
    private readonly IAuthenticationService _service = service;

    /// <summary>
    /// Handles user sign-in requests.
    /// Validates the model, attempts login and returns appropriate result.
    /// </summary>
    /// <param name="credentials">User data transfer object containing login details</param>
    /// <returns>HTTP response with sign-in result</returns>
    /// <exception cref="InvalidDataException">Thrown when input data validation fails.</exception>
    [HttpPost("signin")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult SignIn([FromForm] SignInRequestDTO credentials)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("{Controller} : {Method} - Invalid model state", nameof(AuthenticationController), nameof(SignIn));
                return BadRequest(ModelState);
            }
            UserResponseDTO response = _service.Login(credentials);
            if (response != null)
            {
                _logger.LogInformation("{Controller} : {Method} - User {Number} logged in successfully", nameof(AuthenticationController), nameof(SignIn), credentials.Number);
                return Accepted(new { message = "Login successful", response });
            }
            else
            {
                _logger.LogWarning("{Controller} : {Method} - Failed login attempt for {Number}", nameof(AuthenticationController), nameof(SignIn), credentials.Number);
                return Unauthorized(new { message = "Invalid number or password"});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Controller} : {Method} - Error during sign-in for {Number}", nameof(AuthenticationController), nameof(SignIn), credentials.Number);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred during sign-in" });
        }
    }
}