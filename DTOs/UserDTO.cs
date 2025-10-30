namespace api.DTOs;

/// <summary>
/// Data Transfer Object representing user credentials for authentication.
/// </summary>
public class UserDTO
{

    /// <summary>
    /// Gets or sets the user's unique number or identifier.
    /// </summary>
    public required string Number { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    public required string Password { get; set; }
}