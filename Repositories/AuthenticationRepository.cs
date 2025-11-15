using api.Interfaces;
using api.Data;
using api.Models;
using api.DTOs.Requests;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

/// <summary>
/// Repository class responsible for authentication data access operations.
/// </summary>
/// <param name="logger">Logger for diagnostic messages.</param>
/// <param name="context">Database context for data access.</param>
public class AuthenticationRepository(ILogger<AuthenticationRepository> logger, DBContext context) : IAuthenticationRepository
{
    #region Fields
    /// <summary>
    /// Logger instance for logging repository operations and errors.
    /// </summary>
    private readonly ILogger<AuthenticationRepository> _logger = logger;

    /// <summary>
    /// Database context instance for accessing data storage.
    /// </summary>
    private readonly DBContext _context = context;
    #endregion Fields
    
    #region Methods
    /// <summary>
    /// Checks if a user exists with the specified credentials.
    /// </summary>
    /// <param name="credentials">Login credentials.</param>
    /// <returns>User entity if found; null otherwise.</returns>
    /// <exception cref="Exception">Throws exception if any error occurs during data access.</exception>
    public async Task<User?> UserExistAsync(SignInRequestDto credentials)
    {
        try
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Number == credentials.Number && u.Password == credentials.Password);
            _logger.LogInformation("{Repository}: {Method} found user: {User}", nameof(AuthenticationRepository), nameof(UserExistAsync), user);
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repository}: Exception occurred in {Method} for Number={Number}", nameof(AuthenticationRepository), nameof(UserExistAsync), credentials.Number);
            return null;
        }
    }
    #endregion Methods
}