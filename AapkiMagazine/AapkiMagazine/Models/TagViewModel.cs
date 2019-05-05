using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AapkiMagazine.Models
{
    public class TagViewModel
    {
       public List<ArticleItem> Articles { get; set; }
       public TagItem Tag { get; set; }
       public CategoryItem Category { get; set; }
       public List<NewsItem> BreakingNewsItems { get; set; }
       public List<TopNewsItem> TopNews { get; set; }
       public PageNotFound PageNotFound { get; set; }
    }
}