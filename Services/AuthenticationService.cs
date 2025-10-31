using api.DTOs;
using api.Interfaces;

namespace api.Services;

/// <summary>
/// Service class implementing authentication business logic.
/// </summary>
/// <param name="logger">Logger for recording diagnostic information.</param>
/// <param name="repository">Repository to validate user existence.</param>
public class AuthenticationService(ILogger<AuthenticationService> logger, IAuthenticationRepository repository) : IAuthenticationService
{
    /// <summary>
    /// Logger instance for logging service operations.
    /// </summary>
    private readonly ILogger<AuthenticationService> _logger = logger;

    /// <summary>
    /// Repository instance for accessing authentication data.
    /// </summary>
    private readonly IAuthenticationRepository _repository = repository;

    /// <summary>
    /// Attempts to log in a user by checking existence via repository.
    /// </summary>
    /// <param name="user">User data transfer object containing login credentials.</param>
    /// <returns>True if the user exists and login is successful; otherwise, false.</returns>
    /// <exception cref="Exception">Throws any exception encountered during repository call.</exception>
    public bool Login(UserDTO user)
    {
        try
        {
            User user = _repository.UserExist(user);
            _logger.LogInformation($"{nameof(AuthenticationService)} : {nameof(Login)}");
            return userExist;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(AuthenticationService)} : {nameof(Login)}");
            throw;
        }
    }
}