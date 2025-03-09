using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FUNewsManagementSystem.BLL.Hubs
{
    public class NewsHub : Hub
    {
        public async Task SendNewsUpdate(NewsArticle article)
        {
            await Clients.All.SendAsync("ReceiveNewsUpdate", new
            {
                newsArticleId = article.NewsArticleId,
                newsTitle = article.NewsTitle,
                headline = article.Headline,
                createdDate = article.CreatedDate,
                newsSource = article.NewsSource,
                newsStatus = article.NewsStatus,
                category = article.Category != null ? new { categoryName = article.Category.CategoryName } : null,
                createdBy = article.CreatedBy != null ? new { accountName = article.CreatedBy.AccountName } : null,
                isStaff = true
            });
        }

        public async Task SendNewsEdit(NewsArticle article)
        {
            await Clients.All.SendAsync("ReceiveNewsEdit", new
            {
                newsArticleId = article.NewsArticleId,
                newsTitle = article.NewsTitle,
                headline = article.Headline,
                createdDate = article.CreatedDate,
                newsSource = article.NewsSource,
                newsStatus = article.NewsStatus,
                category = article.Category != null ? new { categoryName = article.Category.CategoryName } : null,
                createdBy = article.CreatedBy != null ? new { accountName = article.CreatedBy.AccountName } : null,
                isStaff = true
            });
        }

        public async Task SendNewsDelete(int articleId)
        {
            await Clients.All.SendAsync("ReceiveNewsDelete", articleId);
        }
    }
}
