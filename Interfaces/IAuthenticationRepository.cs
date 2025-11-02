using api.DTOs.Requests;
using api.Models;

namespace api.Interfaces;

/// <summary>
/// Interface defining repository methods for authentication lookup and validation.
/// </summary>
public interface IAuthenticationRepository
{
    /// <summary>
    /// Checks if a user exists based on provided credentials.
    /// </summary>
    /// <param name="credentials">Login credentials.</param>
    /// <returns>User entity if found; null otherwise.</returns>
    Task<User?> UserExistAsync(SignInRequestDTO credentials);
}