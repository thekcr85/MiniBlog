using System.ComponentModel.DataAnnotations;

namespace MiniBlogApp.Models;

public class BlogPost
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Summary { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [Required]
    public int AuthorId { get; set; }

    public Author? Author { get; set; }

    public List<Comment> Comments { get; set; } = [];
}
