using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.Models;
using MiniBlogApp.Services.Interfaces;
using MiniBlogApp.ViewModels;

namespace MiniBlogApp.Controllers;

public class AuthorController(IAuthorService authorService) : Controller
{
	// GET: Author
	public async Task<IActionResult> Index(CancellationToken cancellationToken)
	{
		var authors = await authorService.GetAllWithPostsAsync(cancellationToken);
		var authorListItems = authors.Select(a => new AuthorListItemViewModel
		{
			Id = a.Id,
			FirstName = a.FirstName,
			LastName = a.LastName,
			Email = a.Email,
			CreatedAt = a.CreatedAt,
			PostCount = a.BlogPosts?.Count ?? 0,
			PostTitles = a.BlogPosts?.Select(p => p.Title).ToList() ?? []
		}).ToList();

		return View(authorListItems);
	}

	// GET: Author/5
	public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
	{
		var author = await authorService.GetByIdWithPostsAsync(id, cancellationToken);
		if (author == null)
		{
			return NotFound();
		}

		var authorDetail = new AuthorDetailViewModel
		{
			Id = author.Id,
			FirstName = author.FirstName,
			LastName = author.LastName,
			Email = author.Email,
			Biography = author.Biography,
			CreatedAt = author.CreatedAt,
			BlogPosts = author.BlogPosts?.Select(p => new AuthorBlogPostViewModel
			{
				Id = p.Id,
				Title = p.Title,
				PublishedAt = p.CreatedAt
			}).ToList() ?? []
		};

		return View(authorDetail);
	}

	// GET: Author/Create
	public IActionResult Create()
	{
		return View();
	}

	// POST : Author/Create
	[HttpPost]
	public async Task<IActionResult> Create(AuthorCreateViewModel model, CancellationToken cancellationToken)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		var author = new Author
		{
			FirstName = model.FirstName,
			LastName = model.LastName,
			Email = model.Email,
			Biography = model.Biography,
			CreatedAt = DateTime.UtcNow
		};

		await authorService.CreateAsync(author, cancellationToken);
		return RedirectToAction(nameof(Index));
	}

	// GET: Author/Edit/5
	public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
	{
		var author = await authorService.GetByIdAsync(id, cancellationToken);
		if (author == null)
		{
			return NotFound();
		}
		var model = new AuthorEditViewModel
		{
			Id = author.Id,
			FirstName = author.FirstName,
			LastName = author.LastName,
			Email = author.Email,
			Biography = author.Biography,
			CreatedAt = author.CreatedAt
		};
		return View(model);
	}

	// POST: Author/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(AuthorEditViewModel model, CancellationToken cancellationToken)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		var author = await authorService.GetByIdAsync(model.Id, cancellationToken);
		if (author == null)
		{
			return NotFound();
		}

		author.FirstName = model.FirstName;
		author.LastName = model.LastName;
		author.Email = model.Email;
		author.Biography = model.Biography;

		await authorService.UpdateAsync(author, cancellationToken);

		return RedirectToAction(nameof(Details), new { id = model.Id });
	}
}
