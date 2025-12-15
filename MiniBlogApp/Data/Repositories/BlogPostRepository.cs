using MiniBlogApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using MiniBlogApp.Models;

namespace MiniBlogApp.Data.Repositories;

public class BlogPostRepository(ApplicationDbContext applicationDbContext) : RepositoryBase<BlogPost>(applicationDbContext, applicationDbContext.BlogPosts), IBlogPostRepository
{
	public async Task<BlogPost?> GetPostWithCommentsAsync(int id, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.BlogPosts
			.Include(bp => bp.Comments)
			.FirstOrDefaultAsync(bp => bp.Id == id, cancellationToken);
	}

	public async Task<IEnumerable<BlogPost>> GetPostsByAuthor(int authorId, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.BlogPosts
			.Where(bp => bp.AuthorId == authorId)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<BlogPost>> GetLatestPostsAsync(int count = 3, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.BlogPosts
			.OrderByDescending(bp => bp.CreatedAt)
			.Take(count) // Take only the specified number of latest posts
			.AsNoTracking()
			.ToListAsync(cancellationToken); 
	}
}
