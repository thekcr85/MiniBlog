using MiniBlogApp.Models;

namespace MiniBlogApp.Services.Interfaces;

public interface IAuthorService
{
	Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<Author?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
	Task<Author> CreateAsync(Author author, CancellationToken cancellationToken = default);
	Task UpdateAsync(Author author, CancellationToken cancellationToken = default);
	Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
	Task<Author?> GetByIdWithPostsAsync(int id, CancellationToken cancellationToken = default);
	Task<IEnumerable<Author>> GetAllWithPostsAsync(CancellationToken cancellationToken = default);
	Task<bool> ExistsAsync(int authorId, CancellationToken cancellationToken = default);
	Task<Author?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
	Task<IEnumerable<Author>> SearchAuthorsByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
	Task<IEnumerable<Author>> GetLatestAuthorsAsync(int count = 3, CancellationToken cancellationToken = default);
}
