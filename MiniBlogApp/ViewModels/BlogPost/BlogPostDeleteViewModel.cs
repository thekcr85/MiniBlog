namespace MiniBlogApp.ViewModels.BlogPost;

public class BlogPostDeleteViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public int CommentCount { get; set; }
}
