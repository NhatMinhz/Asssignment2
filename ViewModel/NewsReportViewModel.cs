using BusinessObjects;

namespace FUNewsManagementSystem.ViewModel
{
    public class NewsReportViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<NewsArticle> NewsArticles { get; set; }
    }
}
