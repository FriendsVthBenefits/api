using api.DTOs.Requests;
using api.DTOs.Responses;
using api.Interfaces;
using api.Mappers;
using api.Models;

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
    /// Attempts to log in a user by validating credentials.
    /// </summary>
    /// <param name="credentials">Login credentials.</param>
    /// <returns>User profile if successful; null if failed.</returns>
    /// <exception cref="Exception">Throws any exception encountered during repository call.</exception>
    public async Task<UserResponseDTO?> LoginAsync(SignInRequestDTO credentials)
    {
        try
        {
            User? user = await _repository.UserExistAsync(credentials);

            if (user == null)
            {
                _logger.LogWarning("Login failed for {Number}", credentials.Number);
                return null;
            }

            UserResponseDTO response = user.ToResponseDTO();
            _logger.LogInformation("Login successful for {Number}", credentials.Number);
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during login for {Number}", credentials.Number);
            throw;
        }
    }
}