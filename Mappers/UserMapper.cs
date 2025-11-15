using api.DTOs.Responses;
using api.Models;

namespace api.Mappers;

/// <summary>
/// Mapper for converting User entity to DTOs.
/// </summary>
public static class UserMapper
{
    /// <summary>
    /// Converts User entity to UserResponseDTO.
    /// </summary>
    /// <param name="user">User entity from database.</param>
    /// <returns>UserResponseDTO for API response.</returns>
    public static UserResponseDto ToResponseDTO(this User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Number = user.Number,
            Mail = user.Mail,
            Gender = (byte)user.Gender,
            Dob = DateTimeOffset.FromUnixTimeSeconds((long)user.Dob).DateTime,
            Location = user.Location,
            ProfilePictureUrl = null,
            Bio = user.Bio,
            Interests = user.Interests,
            LastLogin = DateTimeOffset.FromUnixTimeSeconds((long)user.LastLogin!.Value).DateTime,
            CreatedAt = DateTimeOffset.FromUnixTimeSeconds((long)user.CreatedAt!.Value).DateTime,
            IsActive = (byte)user.IsActive!.Value
        };
    }
}
