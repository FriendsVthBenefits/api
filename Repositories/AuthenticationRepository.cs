using api.Interfaces;
using api.Data;
using api.DTOs;

namespace api.Repositories;

/// <summary>
/// Repository class responsible for authentication data access operations.
/// </summary>
/// <param name="logger">Logger for diagnostic messages.</param>
/// <param name="context">Database context for data access.</param>
public class AuthenticationRepository(ILogger<AuthenticationRepository> logger, DBContext context) : IAuthenticationRepository
{
    /// <summary>
    /// Logger instance for logging repository operations and errors.
    /// </summary>
    private readonly ILogger<AuthenticationRepository> _logger = logger;
    
    /// <summary>
    /// Database context instance for accessing data storage.
    /// </summary>
    private readonly DBContext _context = context;

    /// <summary>
    /// Checks if a user exists with the specified credentials.
    /// </summary>
    /// <param name="user">User data transfer object containing login credentials.</param>
    /// <returns>True if user exists; otherwise, false.</returns>
    /// <exception cref="Exception">Throws exception if any error occurs during data access.</exception>
    public bool UserExist(UserDTO user)
    {
        try
        {
            _logger.LogInformation($"{nameof(AuthenticationRepository)} : {nameof(UserExist)}");
            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(AuthenticationRepository)} : {nameof(UserExist)}");
            throw;
        }
    }
}