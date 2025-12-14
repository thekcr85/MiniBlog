using MiniBlogApp.Models;

namespace MiniBlogApp.Data.Interfaces;

public interface IAuthorRepository : IRepositoryBase<Author>
{
	Task<Author?> GetAuthorWithPostsAsync(int authorId, CancellationToken cancellationToken = default);
	Task<IEnumerable<Author>> GetAllAuthorsWithPostsAsync(CancellationToken cancellationToken = default);
	Task<bool> AuthorExistsAsync(int authorId, CancellationToken cancellationToken = default);
	Task<Author?> GetAuthorByEmailAsync(string email, CancellationToken cancellationToken = default);
	Task<IEnumerable<Author>> SearchAuthorsByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
	Task<IEnumerable<Author>> GetLatestAuthorsAsync(int count = 3, CancellationToken cancellationToken = default);
}

