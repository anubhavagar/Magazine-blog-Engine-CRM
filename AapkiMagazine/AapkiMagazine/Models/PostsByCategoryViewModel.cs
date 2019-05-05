using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AapkiMagazine.Models
{
    public class PostsByCategoryViewModel
    {
        public List<ArticleItem> Articles { get; set; }
        public CategoryItem Category { get; set; }
        public List<NewsItem> BreakingNewsItems { get; set; }
        public List<RelatedNewsItem> RelatedNews { get; set; }
        public PageNotFound PageNotFound { get; set; }
    }

    public class RelatedNewsItem
    {
        public int Article_id { get; set; }
        public string Title_SEO { get; set; }
        public string Title { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Image_Path { get; set; }

    }
}