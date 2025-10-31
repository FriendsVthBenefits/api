namespace api.DTOs.Responses;

/// <summary>
/// Data Transfer Object containing user profile information for API responses.
/// </summary>
public class UserResponseDTO
{
    /// <summary>
    /// Primary key, uniquely identifies each user record.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The user's full name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The user's mobile number (intended for Indian numbers with +91 code).
    /// </summary>
    public long Number { get; set; }

    /// <summary>
    /// The user's email address (used for login and communication).
    /// </summary>
    public string Mail { get; set; } = null!;

    /// <summary>
    /// Gender of the user (use encoded values, e.g., 1=Male, 0=Female).
    /// </summary>
    public byte Gender { get; set; }

    /// <summary>
    /// Date of birth (stored as decimal timestamp).
    /// </summary>
    public DateTime Dob { get; set; }

    /// <summary>
    /// Location or address of the user.
    /// </summary>
    public string Location { get; set; } = null!;

    /// <summary>
    /// URL to retrieve the user's profile picture.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Short biography or personal description.
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// User interests or hobbies (comma-separated string).
    /// </summary>
    public string? Interests { get; set; }

    /// <summary>
    /// Timestamp of last login activity.
    /// </summary>
    public DateTime LastLogin { get; set; }

    /// <summary>
    /// Timestamp when the user record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Indicates if the user account is active (1=active, 0=inactive).
    /// </summary>
    public bool IsActive { get; set; }
}
