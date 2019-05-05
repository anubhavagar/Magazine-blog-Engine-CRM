using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AapkiMagazine.Models;
using AapkiMagazine.DataLayer;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace AapkiMagazine.Controllers
{
   
    public class TagController : Controller
    {
        //
        // GET: /Tag/

        [OutputCache(Duration = 1800, Location = OutputCacheLocation.Server, VaryByParam = "tagged")]
        public ActionResult Index(string tagged)
        {
            TagViewModel tagViewModel = null;
            List<ArticleItem> articles = null;
            CategoryItem category = null;
            List<NewsItem> breakingNewsItems = null;
            DataAccessLayer datalayer = null;
            TagItem tagitem = null;
            List<TopNewsItem> topNews = null;            

            datalayer = new DataAccessLayer();
            
            //check if tag is valid 
            if (!string.IsNullOrEmpty(tagged))
            {                

                articles = datalayer.GetArticlesByTag(tagged);

                if (articles != null)
                {                    

                    if (articles.Count > 0)
                    {
                        tagitem = new TagItem { name = tagged };
                        category = new CategoryItem { Id = articles[0].Category.Id, Name = articles[0].Category.Name, Name_english = articles[0].Category.Name_english };
                    }
                }
            }



            tagViewModel = new TagViewModel();
            tagViewModel.Articles = articles;
            tagViewModel.Category = category;
            tagViewModel.Tag = tagitem;

            breakingNewsItems = datalayer.GetBreakingNews();
            tagViewModel.BreakingNewsItems = breakingNewsItems;
            
            topNews = datalayer.GetTopNews(5);
            tagViewModel.TopNews = topNews;

            return View(tagViewModel);
        }


        [HttpPost]
        public JsonResult TagData()
        {
            List<TagItem> tags = null;
            DataAccessLayer datalayer = null;
            datalayer = new DataAccessLayer();
            tags = datalayer.GetTagList();

            // List<string> output = new List<string>();

            //foreach (TagItem tag in tags) {
            //    string a  = tag.Name;
            //    output.Add(a);
            //}


            //return Json(tags.Select(p => new { TagId = p.Id, TagName = p.Name }), JsonRequestBehavior.AllowGet);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string json = js.Serialize(tags);

            return Json(json, JsonRequestBehavior.AllowGet);
            //string[][] newKeys = output.Select(x => new string[] { x }).ToArray();

            //string json = JsonConvert.SerializeObject(newKeys);
        }

      
    }
}
