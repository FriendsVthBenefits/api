using api.DTOs;

namespace api.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    void Login(UserDTO user);
}