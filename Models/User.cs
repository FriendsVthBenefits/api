using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

/// <summary>
/// Represents a User entity in the application. Stores user profile information, authentication credentials, activity state, and timestamps for auditing.
/// </summary>
[Table("User")]
public partial class User
{
    /// <summary>
    /// Primary key, uniquely identifies each user record.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// The user's full name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The user's mobile number (intended for Indian numbers with +91 code).
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// The user's email address (used for communication).
    /// </summary>
    public string Mail { get; set; } = null!;

    /// <summary>
    /// Password for authentication.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Gender of the user (use encoded values, e.g., 1=Male, 0=Female).
    /// </summary>
    public decimal Gender { get; set; }

    /// <summary>
    /// Date of birth (stored as decimal timestamp).
    /// </summary>
    public decimal Dob { get; set; }

    /// <summary>
    /// Location or address of the user.
    /// </summary>
    public string Location { get; set; } = null!;

    /// <summary>
    /// Profile picture stored as byte array.
    /// </summary>
    public byte[] Pic { get; set; } = null!;

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
    public decimal? LastLogin { get; set; }

    /// <summary>
    /// Timestamp when the user record was created.
    /// </summary>
    public decimal? CreatedAt { get; set; }

    /// <summary>
    /// Timestamp of the most recent update to the user record.
    /// </summary>
    public decimal? UpdatedAt { get; set; }

    /// <summary>
    /// Indicates if the user account is active (1=active, 0=inactive).
    /// </summary>
    public decimal? IsActive { get; set; }

    /// <summary>
    /// Role value for the user (e.g., 1=User, 0=Admin).
    /// </summary>
    public decimal? Role { get; set; }
}
