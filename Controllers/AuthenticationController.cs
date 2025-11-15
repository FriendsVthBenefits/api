using api.DTOs.Requests;
using api.DTOs.Responses;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

/// <summary>
/// Controller that manages user authentication operations.
/// </summary>
/// <param name="logger">Logger for logging diagnostic information.</param>
/// <param name="service">Authentication service for handling signin logic.</param>
[ApiController]
[Route("[controller]")]
public class AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService service) : ControllerBase
{
    #region Fields
    /// <summary>
    /// Logger instance for recording logs through the controller lifecycle.
    /// </summary>
    private readonly ILogger<AuthenticationController> _logger = logger;

    /// <summary>
    /// Instance of the authentication service for business signin related to authentication.
    /// </summary>
    private readonly IAuthenticationService _service = service;
    #endregion Fields
    
    #region Actions
    /// <summary>
    /// Handles user sign-in requests.
    /// </summary>
    /// <param name="credentials">credentials containing signin details</param>
    /// <returns>User profile if successful; error message if failed.</returns>
    /// <exception cref="Exception">Throws any exception encountered during service call.</exception>
    [HttpPost("signin")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignInAsync([FromForm] SignInRequestDto credentials)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("{Controller} : {Method} - Invalid model state", nameof(AuthenticationController), nameof(SignIn));
                return BadRequest(ModelState);
            }

            UserResponseDto? userResponseDTO = await _service.LoginAsync(credentials);

            if (userResponseDTO != null)
            {
                _logger.LogInformation("{Controller} : {Method} - User {Number} logged in successfully", nameof(AuthenticationController), nameof(SignIn), credentials.Number);
                return Accepted(new { message = "Login successful", user = userResponseDTO });
            }

            _logger.LogWarning("{Controller} : {Method} - Failed login attempt for {Number}", nameof(AuthenticationController), nameof(SignIn), credentials.Number);
            return Unauthorized(new { message = "Invalid number or password" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Controller} : {Method} - Error during sign-in for {Number}", nameof(AuthenticationController), nameof(SignIn), credentials.Number);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred during sign-in" });
        }
    #endregion Actions   
    }
}