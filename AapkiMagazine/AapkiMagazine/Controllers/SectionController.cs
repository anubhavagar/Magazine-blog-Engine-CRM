using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AapkiMagazine.Models;
using AapkiMagazine.DataLayer;
using System.Web.UI;

namespace AapkiMagazine.Controllers
{
   
    public class SectionController : Controller
    {
        //
        // GET: /Section/

        [OutputCache(Duration = 1800, Location = OutputCacheLocation.Server, VaryByParam = "categoryType")]
        public ActionResult Index(string categoryType)
        {
            PostsByCategoryViewModel ByCategoryVM = null;
            List<ArticleItem> articles = null;
            DataAccessLayer datalayer = null;
            CategoryItem category = null;
            List<NewsItem> breakingNewsItems = null;
            List<RelatedNewsItem> relatedNews = null;

            //check if categoryType is valid category
            if (!string.IsNullOrEmpty(categoryType))
            {
                datalayer = new DataAccessLayer();

                articles = datalayer.GetArticlesByCategory(categoryType);

                if (articles != null)
                {

                    if (articles.Count > 0)
                    {
                        category = new CategoryItem { Id = articles[0].Category.Id, Name = articles[0].Category.Name, Name_english = articles[0].Category.Name_english };
                    }

                }

                //populate breaking news
                breakingNewsItems = datalayer.GetBreakingNews();



                relatedNews = datalayer.GetRelatedNews(categoryType, 5);

            }
            else
            {

                //redirect to home page for empty or null category input
                return RedirectToAction("Index", "Home");
            }



            ByCategoryVM = new PostsByCategoryViewModel();
            ByCategoryVM.Articles = articles;
            ByCategoryVM.Category = category;
            ByCategoryVM.BreakingNewsItems = breakingNewsItems;
            ByCategoryVM.RelatedNews = relatedNews;

            return View(ByCategoryVM);
        }


        [HttpPost]
        public ActionResult GetLatestNews()
        {
            try
            {               
                List<TopNewsItem> topNews = null;
                DataAccessLayer dataLayer = new DataAccessLayer();
                topNews = dataLayer.GetTopNews(5);

                return this.Json(topNews);
            }
            catch (Exception ex)
            {
                // Throw Exception
                return Json(new { success = false });
            }
        }
    }
}
