using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects;

public partial class Tag
{
    public int TagId { get; set; }

    [Display(Name = "Tag Name")]
    public string? TagName { get; set; }

    [Display(Name = "Note")]
    public string? Note { get; set; }
    [JsonIgnore]
    public virtual ICollection<NewsTag> NewsTags { get; set; } = new List<NewsTag>();
}
