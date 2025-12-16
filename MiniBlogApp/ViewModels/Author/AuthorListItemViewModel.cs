namespace MiniBlogApp.ViewModels.Author;

public class AuthorListItemViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int PostCount { get; set; }
    public List<AuthorPostListItemViewModel> Posts { get; set; } = new();

    public string FullName => $"{FirstName} {LastName}";
}

public class AuthorPostListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}
