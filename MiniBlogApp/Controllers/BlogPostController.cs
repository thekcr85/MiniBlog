using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.Services.Interfaces;
using MiniBlogApp.ViewModels.BlogPost;
using MiniBlogApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniBlogApp.Controllers
{
	public class BlogPostController(IBlogPostService blogPostService, IAuthorService authorService) : Controller
	{
		// GET: BlogPost
		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var blogPosts = await blogPostService.GetAllAsync(cancellationToken);
			var viewModels = blogPosts
				.OrderByDescending(p => p.CreatedAt)
				.Select(p => new BlogPostListItemViewModel
				{
					Id = p.Id,
					Title = p.Title,
					Summary = p.Summary,
					CreatedAt = p.CreatedAt,
					UpdatedAt = p.UpdatedAt,
					AuthorId = p.AuthorId,
					AuthorName = p.Author != null ? $"{p.Author.FirstName} {p.Author.LastName}" : string.Empty,
					CommentCount = p.Comments?.Count ?? 0
				})
				.ToList();

			return View(viewModels);
		}

		// GET: BlogPost/Details/5
		public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
		{
			var post = await blogPostService.GetByIdAsync(id, cancellationToken);
			if (post == null) return NotFound();

			var model = new BlogPostDetailViewModel
			{
				Id = post.Id,
				Title = post.Title,
				Summary = post.Summary,
				Content = post.Content,
				CreatedAt = post.CreatedAt,
				UpdatedAt = post.UpdatedAt,
				AuthorId = post.AuthorId,
				AuthorName = post.Author != null ? $"{post.Author.FirstName} {post.Author.LastName}" : string.Empty,
				Comments = post.Comments?.Select(c => new BlogPostCommentViewModel
				{
					Id = c.Id,
					Content = c.Content,
					AuthorName = c.AuthorName,
					AuthorEmail = c.AuthorEmail,
					CreatedAt = c.CreatedAt
				}).ToList() ?? new List<BlogPostCommentViewModel>()
			};

			return View(model);
		}

		// GET: BlogPost/Create
		public async Task<IActionResult> Create(CancellationToken cancellationToken)
		{
			var authors = await authorService.GetAllAsync(cancellationToken);
			ViewBag.Authors = new SelectList(authors.Select(a => new { a.Id, FullName = $"{a.FirstName} {a.LastName}" }), "Id", "FullName");
			return View(new BlogPostCreateViewModel());
		}

		// POST: BlogPost/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BlogPostCreateViewModel model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				var authors = await authorService.GetAllAsync(cancellationToken);
				ViewBag.Authors = new SelectList(authors.Select(a => new { a.Id, FullName = $"{a.FirstName} {a.LastName}" }), "Id", "FullName");
				return View(model);
			}

			var post = new BlogPost
			{
				Title = model.Title,
				Summary = model.Summary,
				Content = model.Content,
				AuthorId = model.AuthorId,
				CreatedAt = DateTime.UtcNow
			};

			await blogPostService.CreateAsync(post, cancellationToken);
			return RedirectToAction(nameof(Index));
		}

		// GET: BlogPost/Edit/5
		public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
		{
			var post = await blogPostService.GetByIdAsync(id, cancellationToken);
			if (post == null) return NotFound();

			var authors = await authorService.GetAllAsync(cancellationToken);
			ViewBag.Authors = new SelectList(authors.Select(a => new { a.Id, FullName = $"{a.FirstName} {a.LastName}" }), "Id", "FullName", post.AuthorId);

			var model = new BlogPostEditViewModel
			{
				Id = post.Id,
				Title = post.Title,
				Summary = post.Summary,
				Content = post.Content,
				AuthorId = post.AuthorId,
				CreatedAt = post.CreatedAt,
				UpdatedAt = post.UpdatedAt
			};
			return View(model);
		}

		// POST: BlogPost/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(BlogPostEditViewModel model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				var authors = await authorService.GetAllAsync(cancellationToken);
				ViewBag.Authors = new SelectList(authors.Select(a => new { a.Id, FullName = $"{a.FirstName} {a.LastName}" }), "Id", "FullName", model.AuthorId);
				return View(model);
			}

			var post = await blogPostService.GetByIdAsync(model.Id, cancellationToken);
			if (post == null) return NotFound();

			post.Title = model.Title;
			post.Summary = model.Summary;
			post.Content = model.Content;
			post.AuthorId = model.AuthorId;
			post.UpdatedAt = DateTime.UtcNow;

			await blogPostService.UpdateAsync(post, cancellationToken);
			return RedirectToAction(nameof(Details), new { id = model.Id });
		}

		// GET: BlogPost/Delete/5
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var post = await blogPostService.GetByIdAsync(id, cancellationToken);
			if (post == null) return NotFound();

			var model = new BlogPostDeleteViewModel
			{
				Id = post.Id,
				Title = post.Title,
				Summary = post.Summary,
				CreatedAt = post.CreatedAt,
				AuthorName = post.Author != null ? $"{post.Author.FirstName} {post.Author.LastName}" : string.Empty,
				CommentCount = post.Comments?.Count ?? 0
			};

			return View(model);
		}

		// POST: BlogPost/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
		{
			var success = await blogPostService.DeleteAsync(id, cancellationToken);
			if (!success)
			{
				ModelState.AddModelError(string.Empty, "Could not delete blog post.");
				var post = await blogPostService.GetByIdAsync(id, cancellationToken);
				if (post == null) return NotFound();
				var model = new BlogPostDeleteViewModel
				{
					Id = post.Id,
					Title = post.Title,
					Summary = post.Summary,
					CreatedAt = post.CreatedAt,
					AuthorName = post.Author != null ? $"{post.Author.FirstName} {post.Author.LastName}" : string.Empty,
					CommentCount = post.Comments?.Count ?? 0
				};
				return View("Delete", model);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
