using api.DTOs;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

/// <summary>
/// 
/// </summary>
public class AuthenticationController(ILogger<AuthenticationController> logger) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<AuthenticationController> _logger = logger;
    public void SignUp(User user)
    { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userDTO"></param>
    public void SignIn(UserDTO userDTO)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogDebug("{Controller} : {Method} : {DTO}", nameof(AuthenticationController), nameof(SignIn), userDTO);
                throw new InvalidDataException("Invalid Input Data");
            }
            else
            {
                // service call
                _logger.LogInformation($"{nameof(AuthenticationController)} : {nameof(SignIn)}");
            }
        }
        catch(Exception e)
        {
            _logger.LogError(e, $"{nameof(AuthenticationController)} : {nameof(SignIn)}");
        }
    }
    
}