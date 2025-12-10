using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.Services.Interfaces;

namespace MiniBlogApp.Controllers;

public class AuthorController(IAuthorService authorService) : Controller
{
	public async Task<IActionResult> Index(CancellationToken cancellationToken)
	{
		var authors = await authorService.GetAllWithPostsAsync(cancellationToken);
		return View(authors);
	}
}
