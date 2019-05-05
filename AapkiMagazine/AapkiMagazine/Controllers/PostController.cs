using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AapkiMagazine.DataLayer;
using AapkiMagazine.Models;
using AapkiMagazine.Helper;
using System.Web.UI;

namespace AapkiMagazine.Controllers
{
    
    public class PostController : Controller
    {
        //
        // GET: /Post/

        [OutputCache(Duration = 1800, Location = OutputCacheLocation.Server, VaryByParam = "articleId")]
        public ActionResult Index(string articleId, string articleTitle)
        {
            DataAccessLayer datalayer = null;
            ArticleItem articleItem = null;
            PostViewModel PostVM = null;
            List<NewsItem> breakingNewsItems = null;
            List<TagItem> tags = null;
            List<RelatedNewsItem> relatedNews = null;
            string expectedTitle = string.Empty;
            string actualTitle = string.Empty;
          
            int articleID_int;
            datalayer = new DataAccessLayer();

            //check if input is valid integer
            if (int.TryParse(articleId, out articleID_int) && articleID_int >= 1)
            {                
                articleItem = datalayer.GetArticleDetails(articleID_int);

                PostVM = new PostViewModel();


                if (articleItem != null)
                {
                    PostVM.ArticleItem = articleItem;
                    //populate tags
                    if (!string.IsNullOrEmpty(articleItem.Tag_Article))
                    {

                        tags = new List<TagItem>();
                        var tagSplit = articleItem.Tag_Article.Split(',');
                        for (var i = 0; i < tagSplit.Length - 1; i++)
                        {
                            TagItem tagitem = new TagItem { id = i, name = tagSplit[i] };
                            tags.Add(tagitem);
                        }
                        PostVM.ArticleItem.Tags = tags;
                    }
                    if (articleItem.Category != null) {
                        if (!string.IsNullOrEmpty(articleItem.Category.Name_english))
                        {
                            relatedNews = datalayer.GetRelatedNews(articleItem.Category.Name_english, 5);
                            PostVM.RelatedNews = relatedNews;
                        }
                    }

                    

                    if (!string.IsNullOrEmpty(articleItem.Title_SEO))
                    {
                        expectedTitle = articleItem.Title_SEO;
                    }

                    actualTitle = (articleTitle ?? "").ToLower();

                    // permanently redirect to the correct URL
                    if (expectedTitle != actualTitle)
                    {
                        return RedirectToActionPermanent("Index", "Post", new { articleId = articleItem.ID, articleTitle = expectedTitle });
                    }

                }
            }           

          
            //populate breaking news
            breakingNewsItems = datalayer.GetBreakingNews();
            if (breakingNewsItems != null)
            {
                PostVM.BreakingNewsItems = breakingNewsItems;
            }
            else
            {
                PostVM.BreakingNewsItems = new List<NewsItem>();
            }

            
           
            


            return View(PostVM);
        }



    }
}
