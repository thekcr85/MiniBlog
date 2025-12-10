using System.ComponentModel.DataAnnotations;

namespace MiniBlogApp.Models;

public class Author
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Biography { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public List<BlogPost> BlogPosts { get; set; } = [];
}
