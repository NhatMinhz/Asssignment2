using FUNewsManagementSystem.DAL.Models;
using FUNewsManagementSystem.BLL.Hubs;
using FUNewsManagementSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly INewArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly IHubContext<NewsHub> _newsHub;

        public IndexModel(INewArticleService newsArticleService, ICategoryService categoryService, IHubContext<NewsHub> newsHub)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _newsHub = newsHub;
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

        public async Task<IActionResult> OnPostDeleteAsync(int articleId)
        {
            var article = await _newsArticleService.GetNewsArticleByIdAsync(articleId.ToString());
            if (article == null)
            {
                return NotFound();
            }

            await _newsArticleService.DeleteNewsArticleAsync(articleId.ToString());

            // Notify clients via SignalR
            await _newsHub.Clients.All.SendAsync("ReceiveNewsDelete", articleId);

            return RedirectToPage("/NewsArticles/Index");
        }
    }
}
