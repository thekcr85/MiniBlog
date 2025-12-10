using MiniBlogApp.Data.Interfaces;
using MiniBlogApp.Models;
using MiniBlogApp.Services.Interfaces;

namespace MiniBlogApp.Services.Implementations;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
	public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await authorRepository.GetAllAsync(cancellationToken);
	}

	public async Task<Author?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		return await authorRepository.GetByIdAsync(id, cancellationToken);
	}

	public async Task<Author> CreateAsync(Author author, CancellationToken cancellationToken = default)
	{
		await authorRepository.AddAsync(author, cancellationToken);
		return author;
	}

	public async Task UpdateAsync(Author author, CancellationToken cancellationToken = default)
	{
		authorRepository.Update(author);
		await Task.CompletedTask;
	}

	public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		var author = await authorRepository.GetByIdAsync(id, cancellationToken);
		if (author == null)
		{
			return false;
		}
		authorRepository.Remove(author);
		return true;
	}

	public async Task<Author?> GetByIdWithPostsAsync(int id, CancellationToken cancellationToken = default)
	{
		return await authorRepository.GetAuthorWithPostsAsync(id, cancellationToken);
	}

	public async Task<IEnumerable<Author>> GetAllWithPostsAsync(CancellationToken cancellationToken = default)
	{
		return await authorRepository.GetAllAuthorsWithPostsAsync(cancellationToken);
	}

	public async Task<bool> ExistsAsync(int authorId, CancellationToken cancellationToken = default)
	{
		return await authorRepository.AuthorExistsAsync(authorId, cancellationToken);
	}

	public async Task<Author?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		return await authorRepository.GetAuthorByEmailAsync(email, cancellationToken);
	}
}
