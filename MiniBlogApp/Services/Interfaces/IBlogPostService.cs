using MiniBlogApp.Models;

namespace MiniBlogApp.Services.Interfaces;

public interface IBlogPostService
{
	Task<IEnumerable<BlogPost>> GetAllAsync(CancellationToken cancellationToken = default);

	Task<BlogPost?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
	Task<BlogPost> CreateAsync(BlogPost blogPost, CancellationToken cancellationToken = default);
	Task UpdateAsync(BlogPost blogPost, CancellationToken cancellationToken = default);
	Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

	Task<BlogPost?> GetPostWithCommentsAsync(int id, CancellationToken cancellationToken = default);
	Task<IEnumerable<BlogPost>> GetPostsByAuthor(int authorId, CancellationToken cancellationToken = default);
	Task<IEnumerable<BlogPost>> GetLatestPostsAsync(int count = 3, CancellationToken cancellationToken = default);
}
