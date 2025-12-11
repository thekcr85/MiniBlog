using System.ComponentModel.DataAnnotations;

namespace MiniBlogApp.Models;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<BlogPost> BlogPosts { get; set; } = [];
}
