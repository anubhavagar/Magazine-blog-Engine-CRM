using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AapkiMagazine.DataEntity.prod;

namespace AapkiMagazine.Models
{
    public class HomeViewModel
    {

       public List<ArticleItem> HomePageCat1 { get; set; }
       public List<ArticleItem> HomePageCat2 { get; set; }
       public List<ArticleItem> HomePageCat3 { get; set; }
       public List<ArticleItem> HomePageCat4 { get; set; }
       public List<ArticleItem> CorouselData { get; set; }
       public List<ArticleItem> HomePageCat5 { get; set; }
       public List<ArticleItem> HomePageCat6 { get; set; }
       //public List<ArticleItem> Articles { get; set; }
       public List<TagItem> Tags { get; set; }
       public List<NewsItem> BreakingNewsItems { get; set; }
       public List<TopNewsItem> TopNews { get; set; }
       public List<ArticleItem> RelatedNews { get; set; }
       
    }

    public class TopNewsItem
    {
        public int Article_id { get; set; }
        public string Title_SEO { get; set; }
        public string Title { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Image_Path { get; set; }

    }

    public class MenuModel {
        public List<ArticleItem> Articles_desh { get; set; }
        public List<ArticleItem> Articles_videsh { get; set; }
        public List<ArticleItem> Articles_kavitayen { get; set; }
    
    }

    public class ErrorViewModel {
       public List<NewsItem> BreakingNewsItems { get; set; }
    
    }
}