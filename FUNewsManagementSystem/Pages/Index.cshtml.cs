using BusinessObjects;
using FUNewsManagementSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INewArticleService _newsArticleService;

        public IndexModel(ILogger<IndexModel> logger, INewArticleService newsArticleService)
        {
            _logger = logger;
            _newsArticleService = newsArticleService;
        }

        public List<NewsArticle> LatestNews { get; set; } = new();

        public async Task OnGetAsync()
        {
            LatestNews = (await _newsArticleService.GetLatestNews(12)).ToList();
        }
    }
}
