using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly INewArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;

        public IndexModel(INewArticleService newsArticleService, ICategoryService categoryService)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
        }

        public List<NewsArticle> NewsArticles { get; set; } = new();
        public List<Category> Categories { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchTitle { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int? CategoryFilter { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = (await _categoryService.GetAllCategoriesAsync()).ToList();
            var articles = await _newsArticleService.GetFilteredNewsArticlesAsync(SearchTitle, CategoryFilter);

            NewsArticles = articles.ToList();
            return Page();
        }
    }
}
