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
    /// <param name="user">User data transfer object containing login details.</param>
    /// <returns>True if login is successful; otherwise, false.</returns>
    UserResponseDTO Login(SignInRequestDTO credentials);
}