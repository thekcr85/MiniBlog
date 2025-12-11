namespace MiniBlogApp.ViewModels;

public class AuthorDetailViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<AuthorBlogPostViewModel> BlogPosts { get; set; } = [];

    public string FullName => $"{FirstName} {LastName}";
}

public class AuthorBlogPostViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
}
