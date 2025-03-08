using FUNewsManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class ReportModel : PageModel
    {
        private readonly INewArticleService _newArticleService;
        public ReportModel(INewArticleService newArticleService)
        {
            _newArticleService = newArticleService;
        }

        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public NewsReportViewModel NewsReportViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            NewsReportViewModel = new NewsReportViewModel()
            {
                StartDate = StartDate,
                EndDate = EndDate,
                NewsArticles = await _newArticleService.GetNewsReportAsync(StartDate, EndDate)
            };
            return Page();
        }
    }
}
