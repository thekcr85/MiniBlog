using MiniBlogApp.Models;

namespace MiniBlogApp.Data.Interfaces;

public interface IAuthorRepository : IRepositoryBase<Author>
{
	Task<Author?> GetAuthorWithPostsAsync(int authorId, CancellationToken cancellationToken = default);
	Task<IEnumerable<Author>> GetAllAuthorsWithPostsAsync(CancellationToken cancellationToken = default);
	Task<bool> AuthorExistsAsync(int authorId, CancellationToken cancellationToken = default);
	Task<Author?> GetAuthorByEmailAsync(string email, CancellationToken cancellationToken = default);
}

