using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class HistoryModel : PageModel
    {
        private readonly INewArticleService _newArticleService;
        public HistoryModel(INewArticleService newArticleService)
        {
            _newArticleService = newArticleService;
        }

        [BindProperty]
        public List<NewsArticle> StaffNews { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var userIdClaim = User.FindFirst("AccountId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !short.TryParse(userIdClaim, out short userId))
            {
                return Unauthorized();
            }

            // Call service to retrieve news articles
            StaffNews = await _newArticleService.GetNewsArticlesByStaffIdAsync(userId);
            return Page();
        }

    }
}
