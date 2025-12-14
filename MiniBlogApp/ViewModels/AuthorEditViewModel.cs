using System.ComponentModel.DataAnnotations;

namespace MiniBlogApp.ViewModels;

public class AuthorEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 100 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 100 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
    public string Email { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters")]
    public string? Biography { get; set; }

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }
}
