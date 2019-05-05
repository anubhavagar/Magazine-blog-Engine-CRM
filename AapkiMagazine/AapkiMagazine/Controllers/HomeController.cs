using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AapkiMagazine.DataEntity.prod;
using AapkiMagazine.DataLayer;
using AapkiMagazine.Models;
using System.Web.UI;

namespace AapkiMagazine.Controllers
{
     
    public class HomeController : Controller
    {
        [OutputCache(Duration = 1800, Location = OutputCacheLocation.Server, VaryByParam = "none")]       
        public ActionResult Index()
        {           
            DataAccessLayer datalayer = null;
            HomeViewModel homeVM = null;
            List<ArticleItem> HomePageCat1 = null;
            List<ArticleItem> HomePageCat2 = null;
            List<ArticleItem> HomePageCat3 = null;
            List<ArticleItem> HomePageCat4 = null;
            List<ArticleItem> HomePageCat5 = null;
            List<ArticleItem> HomePageCat6 = null;
            List<ArticleItem> corouselData = null;
            List<TagItem> tags = null;
            List<NewsItem> breakingNewsItems = null;
            List<TopNewsItem> topNews = null;
            List<ArticleItem> articles = null;
            List<ArticleItem> relatedNews = null;


            datalayer = new DataAccessLayer();
            articles = datalayer.GetHomePageData();

            tags = datalayer.GetTagList();

            if (articles != null)
            {
                if (articles.Count > 0)
                {
                    HomePageCat1 = articles.Where(a => a.Category.Id == 13).ToList();
                    HomePageCat2 = articles.Where(a => a.Category.Id == 9).ToList();
                    HomePageCat3 = articles.Where(a => a.Category.Id == 8).ToList();
                    HomePageCat4 = articles.Where(a => a.Category.Id == 11).ToList();
                    HomePageCat5 = articles.Where(a => a.Category.Id == 10).ToList();
                    HomePageCat6 = articles.Where(a => a.Category.Id == 12).ToList();
                    corouselData = articles.Where(a => (!string.IsNullOrEmpty(a.Image_path))).ToList();
                }
            }
            else
            {

                HomePageCat1 = new List<ArticleItem>();
                HomePageCat2 = new List<ArticleItem>();
                HomePageCat3 = new List<ArticleItem>();
                HomePageCat4 = new List<ArticleItem>();
                corouselData = new List<ArticleItem>();

            }

            if (tags != null)
            {

            }
            else
            {
                tags = new List<TagItem>();
            }

            breakingNewsItems = datalayer.GetBreakingNews();

            topNews = datalayer.GetTopNews(7);
            if (topNews == null)
            {
                topNews = new List<TopNewsItem>();
            }

            relatedNews = new List<ArticleItem>();

            if (HomePageCat1 != null) {

                if (HomePageCat1.Count > 0) {

                    relatedNews.Add(HomePageCat1[0]);
                }
            }
            if (HomePageCat2 != null)
            {

                if (HomePageCat2.Count > 0)
                {

                    relatedNews.Add(HomePageCat2[0]);
                }
            }

            if (HomePageCat3 != null)
            {

                if (HomePageCat3.Count > 0)
                {

                    relatedNews.Add(HomePageCat3[0]);
                }
            }

            if (HomePageCat4 != null)
            {

                if (HomePageCat4.Count > 0)
                {

                    relatedNews.Add(HomePageCat4[0]);
                }
            }

            if (HomePageCat5 != null)
            {

                if (HomePageCat5.Count > 0)
                {

                    relatedNews.Add(HomePageCat5[0]);
                }
            }

            if (HomePageCat6 != null)
            {

                if (HomePageCat6.Count > 0)
                {

                    relatedNews.Add(HomePageCat6[0]);
                }
            }
            homeVM = new HomeViewModel();
            homeVM.HomePageCat1 = HomePageCat1;
            homeVM.HomePageCat2 = HomePageCat2;
            homeVM.HomePageCat3 = HomePageCat3;
            homeVM.HomePageCat4 = HomePageCat4;
            homeVM.CorouselData = corouselData;
            homeVM.Tags = tags;
            homeVM.BreakingNewsItems = breakingNewsItems;
            homeVM.TopNews = topNews;
            homeVM.RelatedNews = relatedNews;


            return View(homeVM);
        }

        [HttpPost]
        public ActionResult GetDataByCategory()
        {
            try
            {              

                DataAccessLayer dataLayer = new DataAccessLayer();
                List<ArticleItem> articles_desh = dataLayer.GetArticlesByCategory("desh", 3);
                List<ArticleItem> articles_videsh = dataLayer.GetArticlesByCategory("videsh", 3);
                List<ArticleItem> articles_kavitayen = dataLayer.GetArticlesByCategory("krantikari-kavitayen", 3);
                MenuModel menuModel = new MenuModel { Articles_desh = articles_desh, Articles_videsh = articles_videsh, Articles_kavitayen = articles_kavitayen };

                return this.Json(menuModel);
            }
            catch (Exception ex)
            {
                // Throw Exception
                return Json(new { success = false });
            }
        }
    }
}
