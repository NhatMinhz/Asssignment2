using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FUNewsManagementSystem.BLL.Interfaces;
using FUNewsManagementSystem.DAL.Models;
using FUNewsManagementSystem.ViewModel;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class ReportModel : PageModel
    {
        private readonly INewArticleService _newsArticleService;

        public ReportModel(INewArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime EndDate { get; set; } = DateTime.Now;
        public NewsReportViewModel NewsReportViewModel { get; set; } = new();
        public (SystemAccount CreatedBy, int ArticleCount)? TopContributor { get; set; }
        public (Category CategoryName, int ArticleCount)? MostPopularCategory { get; set; }
        public List<(DateTime Date, int Count)> NewsTrends { get; set; } = new();

        public async Task OnGetAsync(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate ?? StartDate;
            EndDate = endDate ?? EndDate;

            NewsReportViewModel.NewsArticles = await _newsArticleService.GetNewsReportAsync(StartDate, EndDate);

            TopContributor = NewsReportViewModel.NewsArticles
                .GroupBy(n => n.CreatedBy)
                .Select(g => (g.Key, g.Count()))
                .OrderByDescending(g => g.Item2)
                .FirstOrDefault();

            MostPopularCategory = NewsReportViewModel.NewsArticles
                .GroupBy(n => n.Category)
                .Select(g => (g.Key, g.Count()))
                .OrderByDescending(g => g.Item2)
                .FirstOrDefault();

            NewsTrends = NewsReportViewModel.NewsArticles
                .GroupBy(n => n.CreatedDate.Value.Date)
                .Select(g => (g.Key, g.Count()))
                .OrderBy(g => g.Key)
                .ToList();
        }
    }
}
