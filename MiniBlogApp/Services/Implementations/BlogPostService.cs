using MiniBlogApp.Data.Interfaces;
using MiniBlogApp.Models;
using MiniBlogApp.Services.Interfaces;

namespace MiniBlogApp.Services.Implementations;

public class BlogPostService(IBlogPostRepository blogPostRepository) : IBlogPostService
{
	public async Task<IEnumerable<BlogPost>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await blogPostRepository.GetAllAsync(cancellationToken);
	}

	public async Task<BlogPost?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Invalid blog post ID.", nameof(id));
		}

		return await blogPostRepository.GetByIdAsync(id, cancellationToken);
	}

	public async Task<BlogPost> CreateAsync(BlogPost blogPost, CancellationToken cancellationToken = default)
	{
		if (blogPost == null)
		{
			throw new ArgumentNullException(nameof(blogPost), "Blog post cannot be null.");
		}
		await blogPostRepository.AddAsync(blogPost, cancellationToken);
		return blogPost;
	}

	public Task UpdateAsync(BlogPost blogPost, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<BlogPost?> GetPostWithCommentsAsync(int id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<BlogPost>> GetPostsByAuthor(int authorId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<BlogPost>> GetLatestPostsAsync(int count = 3, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
