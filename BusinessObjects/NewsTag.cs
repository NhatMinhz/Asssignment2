using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects;
[PrimaryKey(nameof(NewsArticleId), nameof(TagId))]
public partial class NewsTag
{
    public string NewsArticleId { get; set; } = null!;
    public int TagId { get; set; }
    [JsonIgnore]
    public virtual NewsArticle? NewsArticle { get; set; }
    [JsonIgnore]
    public virtual Tag? Tag { get; set; }
}