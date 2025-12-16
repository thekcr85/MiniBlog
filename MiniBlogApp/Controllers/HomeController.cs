using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.Models;
using MiniBlogApp.Services.Interfaces;
using MiniBlogApp.ViewModels.Author;
using MiniBlogApp.Data.Interfaces;

namespace MiniBlogApp.Controllers
{
    public class HomeController(IAuthorService authorService, IAuthorRepository authorRepository) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var authors = await authorService.GetLatestAuthorsAsync(3, cancellationToken);
            var authorListItems = authors.Select(a => new AuthorListItemViewModel
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                CreatedAt = a.CreatedAt,
                PostCount = a.BlogPosts?.Count ?? 0,
                Posts = a.BlogPosts?.Select(p => new AuthorPostListItemViewModel { Id = p.Id, Title = p.Title }).ToList() ?? new List<AuthorPostListItemViewModel>()
            }).ToList();

            return View(authorListItems);
        }

        public async Task<IActionResult> Search(string searchTerm, CancellationToken cancellationToken = default)
        {
            // Get search results
            var searchResults = new List<AuthorListItemViewModel>();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var authors = await authorService.SearchAuthorsByNameAsync(searchTerm.Trim(), cancellationToken);
                searchResults = authors.Select(a => new AuthorListItemViewModel
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email,
                    CreatedAt = a.CreatedAt,
                    PostCount = a.BlogPosts?.Count ?? 0,
                    Posts = a.BlogPosts?.Select(p => new AuthorPostListItemViewModel { Id = p.Id, Title = p.Title }).ToList() ?? new List<AuthorPostListItemViewModel>()
                }).ToList();
            }

            // Always get latest 3 authors for the sidebar
            var latestAuthors = await authorService.GetLatestAuthorsAsync(3, cancellationToken);
            var latestAuthorListItems = latestAuthors.Select(a => new AuthorListItemViewModel
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                CreatedAt = a.CreatedAt,
                PostCount = a.BlogPosts?.Count ?? 0,
                Posts = a.BlogPosts?.Select(p => new AuthorPostListItemViewModel { Id = p.Id, Title = p.Title }).ToList() ?? new List<AuthorPostListItemViewModel>()
            }).ToList();

            // Pass both to view
            ViewBag.SearchTerm = searchTerm;
            ViewBag.LatestAuthors = latestAuthorListItems;
            return View("Index", searchResults);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
