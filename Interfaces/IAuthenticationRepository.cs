using api.DTOs;

namespace api.Interfaces;

/// <summary>
/// Interface defining repository methods for authentication lookup and validation.
/// </summary>
public interface IAuthenticationRepository
{
    /// <summary>
    /// Checks if a user exists based on provided credentials.
    /// </summary>
    /// <param name="user">User data transfer object containing login details.</param>
    /// <returns>True if the user exists; otherwise, false.</returns>
    bool UserExist(UserDTO user);
}