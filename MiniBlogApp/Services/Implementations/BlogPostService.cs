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

	public async Task UpdateAsync(BlogPost blogPost, CancellationToken cancellationToken = default)
	{
		if (blogPost == null)
		{
			throw new ArgumentNullException(nameof(blogPost), "Blog post cannot be null.");
		}
		await blogPostRepository.UpdateAsync(blogPost, cancellationToken);
	}

	public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		var blogPost = await blogPostRepository.GetByIdAsync(id, cancellationToken);
		if (blogPost == null)
		{
			return false;
		}
		await blogPostRepository.RemoveAsync(blogPost, cancellationToken);
		return true;
	}

	public async Task<BlogPost?> GetPostWithCommentsAsync(int id, CancellationToken cancellationToken = default)
	{
		return await blogPostRepository.GetPostWithCommentsAsync(id, cancellationToken);
	}

	public async Task<IEnumerable<BlogPost>> GetPostsByAuthor(int authorId, CancellationToken cancellationToken = default)
	{
		return await blogPostRepository.GetPostsByAuthor(authorId, cancellationToken);
	}

	public async Task<IEnumerable<BlogPost>> GetLatestPostsAsync(int count = 3, CancellationToken cancellationToken = default)
	{
		return await blogPostRepository.GetLatestPostsAsync(count, cancellationToken);
	}
}
