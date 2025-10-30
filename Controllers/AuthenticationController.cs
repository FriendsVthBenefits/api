using api.DTOs;
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
    /// <param name="user">User data transfer object containing login details</param>
    /// <returns>HTTP response with sign-in result</returns>
    /// <exception cref="InvalidDataException">Thrown when input data validation fails.</exception>
    [HttpPost]
    [Route("/signin")]
    public IActionResult SignIn(UserDTO user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogDebug("{Controller} : {Method} : {DTO}", nameof(AuthenticationController), nameof(SignIn), user);
                throw new InvalidDataException("Invalid Input Data");
            }
            else
            {
                bool userExist = _service.Login(user);
                _logger.LogInformation($"{nameof(AuthenticationController)} : {nameof(SignIn)}");
                return Ok(user);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(AuthenticationController)} : {nameof(SignIn)}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}