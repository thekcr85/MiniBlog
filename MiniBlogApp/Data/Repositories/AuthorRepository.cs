using Microsoft.EntityFrameworkCore;
using MiniBlogApp.Data.Interfaces;
using MiniBlogApp.Models;

namespace MiniBlogApp.Data.Repositories;

public class AuthorRepository(ApplicationDbContext applicationDbContext)
	: RepositoryBase<Author>(applicationDbContext, applicationDbContext.Authors), IAuthorRepository
{
	public async Task<Author?> GetAuthorWithPostsAsync(int authorId, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.Include(a => a.BlogPosts)
			.FirstOrDefaultAsync(a => a.Id == authorId, cancellationToken);
	}

	public async Task<IEnumerable<Author>> GetAllAuthorsWithPostsAsync(CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.Include(a => a.BlogPosts)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	public async Task<bool> AuthorExistsAsync(int authorId, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.AnyAsync(a => a.Id == authorId, cancellationToken);
	}

	public async Task<Author?> GetAuthorByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
	}

	public async Task<IEnumerable<Author>> SearchAuthorsByNameAsync(string searchTerm, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.Where(a => a.FirstName.Contains(searchTerm) || a.LastName.Contains(searchTerm) || a.Email.Contains(searchTerm))
			.Include(a => a.BlogPosts)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<Author>> GetLatestAuthorsAsync(int count = 3, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.OrderByDescending(a => a.CreatedAt)
			.Take(count)
			.Include(a => a.BlogPosts)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}
}
