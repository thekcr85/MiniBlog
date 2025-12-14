namespace MiniBlogApp.ViewModels;

public class AuthorDeleteViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int PostCount { get; set; }
}
