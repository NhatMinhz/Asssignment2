using Microsoft.EntityFrameworkCore;

namespace FUNewsManagementSystem.DAL.Models;
[PrimaryKey(nameof(NewsArticleId), nameof(TagId))]
public partial class NewsTag
{
    public string NewsArticleId { get; set; } = null!;
    public int TagId { get; set; }
    public virtual NewsArticle? NewsArticle { get; set; }
    public virtual Tag? Tag { get; set; }
}