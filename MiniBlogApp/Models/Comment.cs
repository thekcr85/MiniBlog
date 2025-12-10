using System.ComponentModel.DataAnnotations;

namespace MiniBlogApp.Models;

public class Comment
{
    public int Id { get; set; }

    [Required]
    [StringLength(1000)]
    public string Content { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string AuthorName { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(50)]
    public string? AuthorEmail { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public int BlogPostId { get; set; }

    public BlogPost? BlogPost { get; set; }
}
