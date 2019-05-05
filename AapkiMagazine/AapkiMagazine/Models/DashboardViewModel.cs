using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AapkiMagazine.DataEntity.prod;
using System.Web.Mvc;

namespace AapkiMagazine.Models
{
    public class DashboardViewModel
    {
      public List<ArticleItem> ArticleEntryItems { get; set; }
      public List<ArticleItem> ArticlePublishedItems { get; set; }
      public List<ArticleItem> ArticleArchivedItems { get; set; }
      public List<CategoryItem> Categories { get; set; }
      public List<UserProfileItem> UserProfiles { get; set; }
      public UserProfileItem userDetail { get; set; }
      public List<NewsItem> BreakingNews { get; set; }
    }


  
    public class BreakingNews {
        public List<NewsItem> BreakingNewsItems { get; set; }
        //public List<LatestNewsItem> LatestTenNews { get; set; }
    }
    public class ArticleDetail
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Picture_url { get; set; }
        public string Video_url { get; set; }
        //convert to date format
        public string Created_Date { get; set; }
        public string Company { get; set; }
        public List<string> Tag_Article { get; set; }
       

    }

    public class NewsItem
    {
        public int Id { get; set; }
        public string CustomText { get; set; }
        public int? Article_id { get; set; }
        public string Title_SEO { get; set; }
        public string Title { get; set; }
        public IList<SelectListItem> TopNewsDdl { get; set; }

    }
    public class TopBreakingNewsDDL 
    {
        public int Article_id { get; set; }
        public string Title_SEO { get; set; }
    
    }
}