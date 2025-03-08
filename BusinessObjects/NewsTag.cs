using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects;
[PrimaryKey(nameof(NewsArticleId), nameof(TagId))]
public partial class NewsTag
{
    public string NewsArticleId { get; set; } = null!;
    public int TagId { get; set; }
    public virtual NewsArticle? NewsArticle { get; set; }
    public virtual Tag? Tag { get; set; }
}