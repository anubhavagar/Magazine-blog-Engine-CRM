using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AapkiMagazine.Models
{
    public class PostViewModel
    {
        public ArticleItem ArticleItem { get; set; }
        public List<NewsItem> BreakingNewsItems { get; set; }
        public List<RelatedNewsItem> RelatedNews { get; set; }
        public PageNotFound PageNotFound { get; set; }
    }
}