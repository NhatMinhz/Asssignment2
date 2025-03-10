using FUNewsManagementSystem.BLL.Interfaces;
using FUNewsManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class ReportModel : PageModel
    {
        private readonly INewArticleService _newArticleService;
        public ReportModel(INewArticleService newArticleService)
        {
            _newArticleService = newArticleService;
            StartDate = new DateTime(2024, 1, 1); 
            EndDate = DateTime.Now;
        }

        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public NewsReportViewModel NewsReportViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            // Ensure StartDate and EndDate are within valid range
            if (StartDate < new DateTime(1753, 1, 1))
                StartDate = new DateTime(1753, 1, 1);

            if (EndDate < new DateTime(1753, 1, 1))
                EndDate = new DateTime(1753, 1, 1);

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
