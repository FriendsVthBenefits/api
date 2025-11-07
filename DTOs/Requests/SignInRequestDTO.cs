using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Requests;

/// <summary>
/// Data Transfer Object for user authentication signin requests.
/// Validates user credentials with enhanced security constraints.
/// </summary>
public class SignInRequestDTO
{
    /// <summary>
    /// Gets or sets the user's mobile number.
    /// </summary>
    [Required(ErrorMessage = "Mobile number is required.")]
    [Range(1000000000, 9999999999, ErrorMessage = "Mobile number must be a valid 10-digit number.")]
    [Display(Name = "Mobile Number")]
    public long Number { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^()_+\-=\[\]{}|;:',.<>/~`])[A-Za-z\d@$!%*?&#^()_+\-=\[\]{}|;:',.<>/~`]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
