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
            _newArticleService = newArticleService;
            StartDate = new DateTime(2024, 1, 1); 
            EndDate = DateTime.Now;
        }

        public NewsReportViewModel NewsReportViewModel { get; set; } = new();
        public (SystemAccount CreatedBy, int ArticleCount)? TopContributor { get; set; }
        public (Category CategoryName, int ArticleCount)? MostPopularCategory { get; set; }
        public List<(DateTime Date, int Count)> NewsTrends { get; set; } = new();

        public async Task OnGetAsync(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate ?? StartDate;
            EndDate = endDate ?? EndDate;

            var articles = await _newsArticleService
                .GetNewsReportAsync(StartDate, EndDate);

            NewsReportViewModel.NewsArticles = articles
                .Include(n => n.CreatedBy)  // Ensure CreatedBy is included
                .Include(n => n.Category)   // Ensure Category is included
                .ToList();

            TopContributor = NewsReportViewModel.NewsArticles
                .Where(n => n.CreatedBy != null) // Avoid nulls
                .GroupBy(n => n.CreatedBy)
                .Select(g => (g.Key, g.Count()))
                .OrderByDescending(g => g.Item2)
                .FirstOrDefault();

            MostPopularCategory = NewsReportViewModel.NewsArticles
                .Where(n => n.Category != null) // Avoid nulls
                .GroupBy(n => n.Category)
                .Select(g => (g.Key, g.Count()))
                .OrderByDescending(g => g.Item2)
                .FirstOrDefault();

            NewsTrends = NewsReportViewModel.NewsArticles
                .Where(n => n.CreatedDate.HasValue) // Ensure CreatedDate exists
                .GroupBy(n => n.CreatedDate.Value.Date)
                .Select(g => (g.Key, g.Count()))
                .OrderBy(g => g.Key)
                .ToList();
        }
    }
}
