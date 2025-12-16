using MiniBlogApp.Models;

namespace MiniBlogApp.Data.Interfaces;

public interface IBlogPostRepository : IRepositoryBase<BlogPost>
{
	Task<BlogPost?> GetPostWithCommentsAsync(int id, CancellationToken cancellationToken = default);
	Task<IEnumerable<BlogPost>> GetAllWithAuthorAndCommentsAsync(CancellationToken cancellationToken = default);
	Task<IEnumerable<BlogPost>> GetPostsByAuthor(int authorId, CancellationToken cancellationToken = default);
	Task<IEnumerable<BlogPost>> GetLatestPostsAsync(int count = 3, CancellationToken cancellationToken = default);
}
