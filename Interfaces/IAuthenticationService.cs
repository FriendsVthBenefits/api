using api.DTOs.Requests;
using api.DTOs.Responses;

namespace api.Interfaces;

/// <summary>
/// Interface defining authentication service contracts.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Attempts to log in a user with the provided credentials.
    /// </summary>
    /// <param name="credentials">Login credentials.</param>
    /// <returns>User profile if successful; null if failed.</returns>
    Task<UserResponseDto?> LoginAsync(SignInRequestDto credentials);
}