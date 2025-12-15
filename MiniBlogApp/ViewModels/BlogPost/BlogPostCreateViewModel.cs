using System.ComponentModel.DataAnnotations;

namespace MiniBlogApp.ViewModels.BlogPost;

public class BlogPostCreateViewModel
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Summary is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Summary must be between 10 and 500 characters")]
    public string Summary { get; set; } = string.Empty;

    [Required(ErrorMessage = "Content is required")]
    [StringLength(10000, MinimumLength = 20, ErrorMessage = "Content must be between 20 and 10000 characters")]
    public string Content { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required")]
    public int AuthorId { get; set; }
}
