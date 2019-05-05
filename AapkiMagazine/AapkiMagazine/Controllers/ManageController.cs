using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AapkiMagazine.DataLayer;
using AapkiMagazine.Models;
using AapkiMagazine.DataEntity.prod;
using System.Text;
using AapkiMagazine.Helper;
using System.Web.Script.Serialization;

namespace AapkiMagazine.Controllers
{
    //[Authorize]
    public class ManageController : Controller
    {
        //
        // GET: /Manage/

        private const  int CONTENT_SUMMARY_LENGTH = 200;

        public ActionResult Index()
        {
            DataAccessLayer datalayer = null;
            List<ArticleItem> articlesEntry = null;
            List<ArticleItem> articlesPublished = null;
            List<ArticleItem> articlesArchived = null;
            DashboardViewModel dashboardVM = null;
            List<NewsItem> breakingNewsItems = null;
            List<LatestNewsItem> latestTenNews = null;
           

            if (Session["userprofile"] != null)
            {
                UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];

                dashboardVM = new DashboardViewModel();
                dashboardVM.userDetail = userprofile;

                datalayer = new DataAccessLayer();

                // get all articles entry
                articlesEntry = datalayer.GetAllArticlesEntry(userprofile.ID, userprofile.Role);
                if (articlesEntry == null)
                {
                    articlesEntry = new List<ArticleItem>();
                }
                dashboardVM.ArticleEntryItems = articlesEntry;


                // get all articles published
                articlesPublished = datalayer.GetAllArticlesPublished(userprofile.ID, userprofile.Role);
                if (articlesPublished == null)
                {
                    articlesPublished = new List<ArticleItem>();
                }
                dashboardVM.ArticlePublishedItems = articlesPublished;


                // get all articles Archived
                articlesArchived = datalayer.GetAllArticlesArchived(userprofile.ID, userprofile.Role); ;
                if (articlesArchived == null)
                {
                    articlesArchived = new List<ArticleItem>();
                }
                dashboardVM.ArticleArchivedItems = articlesArchived;



                if (dashboardVM.userDetail.Role == "admin" || dashboardVM.userDetail.Role == "ceditor" || dashboardVM.userDetail.Role == "sadmin")
                {   // 1-  get all catagories
                    List<CategoryItem> article_categories = datalayer.GetCategories();
                    dashboardVM.Categories = article_categories;

                    // 2 - get all user profiles
                    List<UserProfileItem> UserProfiles = datalayer.GetAllUsers(userprofile.Role);

                    List<string> roles = new List<string>();

                    if (dashboardVM.userDetail.Role == "sadmin")
                    {
                        roles.Add("sadmin"); roles.Add("admin"); roles.Add("ceditor"); roles.Add("editor"); roles.Add("newuser");
                    }
                    else if(dashboardVM.userDetail.Role == "admin"){
                        roles.Add("admin"); roles.Add("ceditor"); roles.Add("editor"); roles.Add("newuser");
                    }
                    else if (dashboardVM.userDetail.Role == "ceditor")
                    {
                        roles.Add("ceditor"); roles.Add("editor"); roles.Add("newuser");
                    }
                    
                   

                    foreach (UserProfileItem item in UserProfiles)
                    {
                        item.Roles = new List<SelectListItem>();
                        foreach (string role in roles)
                        {
                            SelectListItem selectListItem = new SelectListItem { Text = role, Value = role, Selected = role == item.Role };
                            item.Roles.Add(selectListItem);
                        }

                    }
                    dashboardVM.UserProfiles = UserProfiles;


                    // 3- get breaking news
                    breakingNewsItems = datalayer.GetBreakingNews();

                    //bind drop down with top 10 latest news
                    latestTenNews = datalayer.GetLatestTenNews();

                    foreach (NewsItem newsitem in breakingNewsItems)
                    {
                        newsitem.TopNewsDdl = new List<SelectListItem>();
                        foreach (LatestNewsItem item in latestTenNews)
                        {
                            SelectListItem selectListItem = new SelectListItem { Text = item.title, Value = item.id.ToString(), Selected = item.id == newsitem.Article_id };
                            newsitem.TopNewsDdl.Add(selectListItem);
                        }
                    }



                    dashboardVM.BreakingNews = breakingNewsItems;


                }

            }
            else
            {
                return RedirectToAction("Authenticate", "Account");
            }

            return View(dashboardVM);
        }


   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DashboardViewModel model, FormCollection formValues, string command)
        {
            DataAccessLayer dataLayer = null;
            if (command == "Create New Article")
            {
                return RedirectToAction("CreateArticle", "Manage");
            }
            if (command == "Save Breaking News")
            {
               
                dataLayer = new DataAccessLayer();
                dataLayer.UpdateBreakingNews(model.BreakingNews);
                return RedirectToAction("Index", "Manage");
            }
            if (command == "Save User Profiles")
            {                
                dataLayer = new DataAccessLayer();
                dataLayer.UpdateProfiles(model.UserProfiles);
                return RedirectToAction("Index", "Manage");
            }

            return View(model);

        }
        public ActionResult CreateArticle()
        {

            //populate the categories
            DataAccessLayer dataAccess = new DataAccessLayer();
            List<CategoryItem> article_categories = dataAccess.GetCategories();


            ArticleViewModel articleVM = new ArticleViewModel();
            ArticleItem articleItem = new ArticleItem();
            articleVM.Categories = article_categories;
            articleVM.ArticleItem = articleItem;


            return View(articleVM);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle(ArticleViewModel articleVM, FormCollection formValues, string command)
        {
            DataAccessLayer datalayer = null;
            int articleID = 0;
            List<TagItem> tags = null;

            if (command == "Save & Preview")
            {
                if (ModelState.IsValid)
                {
                    //articleVM.ID = Guid.NewGuid();
                    //articleVM.Created_Date = DateTime.Now;
                    if (Session["UserProfile"] != null)
                    {
                        UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                        articleVM.ArticleItem.Userid = userprofile.ID;

                        if (!string.IsNullOrEmpty(articleVM.ArticleItem.Content))
                        {
                            articleVM.ArticleItem.Content_summary = StringHtmlExtensions.TruncateHtml(articleVM.ArticleItem.Content, 200, "....");
                        }
                        if (!string.IsNullOrEmpty(articleVM.ArticleItem.Title_English))
                        {
                            articleVM.ArticleItem.Title_SEO = articleVM.ArticleItem.Title_English.ToSeoUrl();
                        }



                        //TODO: check if content is empty - return VM

                        datalayer = new DataAccessLayer();
                        articleID = datalayer.CreateArticle(articleVM.ArticleItem);
                        if (articleID != 0)
                        {
                            if (!string.IsNullOrEmpty(articleVM.ArticleItem.Tag_Article))
                            {
                                //update tag table for this article
                                tags = new List<TagItem>();
                                var tagSplit = articleVM.ArticleItem.Tag_Article.Split(',');
                                for (var i = 0; i < tagSplit.Length - 1; i++)
                                {
                                    TagItem tagitem = new TagItem { id = i, name = tagSplit[i] };
                                    tags.Add(tagitem);
                                }
                                bool istagsupdated = datalayer.UpdateTagEntry(tags, articleID);
                                if (!istagsupdated)
                                {
                                    //log error that tag updation failed
                                }

                            }

                            return RedirectToAction("ArticleOption", "Manage", new { articleid = articleID });
                        }
                        else
                        {
                            //some error occured while saving the article
                            return View(articleVM);
                        }
                    }
                    else
                    {
                        return RedirectToAction("Authenticate", "Account");
                    }



                }
            }
            else if (command == "Cancel")
            {
                return RedirectToAction("Index", "Manage");
            }

            return View(articleVM);

        }

        public ActionResult ArticleOption(int articleid)
        {
            ArticleViewModel articleVM = null;
            ArticleItem articleItem = null;
            DataAccessLayer datalayer = null;
            List<TagItem> tags = null;

            //Guid userId;

            articleVM = new ArticleViewModel();

            if (Session["UserProfile"] != null)
            {
                UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                //articleVM.Userprofile = userprofile;
                //userId = userprofile.ID;

                datalayer = new DataAccessLayer();
                articleItem = datalayer.GetArticleEntryDetails(articleid, userprofile.ID);

                if (articleItem != null)
                {

                    articleVM.ArticleItem = articleItem;

                    if (!string.IsNullOrEmpty(articleItem.Tag_Article))
                    {

                        tags = new List<TagItem>();
                        var tagSplit = articleItem.Tag_Article.Split(',');
                        for (var i = 0; i < tagSplit.Length - 1; i++)
                        {
                            TagItem tagitem = new TagItem { id = i, name = tagSplit[i] };
                            tags.Add(tagitem);
                        }
                        articleVM.ArticleItem.Tags = tags;
                    }

                }
                else
                {
                    articleVM = new ArticleViewModel();
                    articleVM.ArticleItem = new ArticleItem();
                    ModelState.AddModelError("", "Article Not Exist or Some error occured while fetching article details.");
                    return View(articleVM);
                }
            }
            else
            {
                return RedirectToAction("Authenticate", "Account");
            }


            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticleOption(ArticleViewModel articleVM, FormCollection formValues, string command, HttpPostedFileBase uplimage)
        {
            DataAccessLayer datalayer = null;
            ArticleItem articleItem = null;
            Guid valid_Articleid = Guid.Empty;
            StringBuilder script = null;
            //Guid userId;

            if (Session["UserProfile"] != null)
            {
                UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
               // userId = userprofile.ID;

                if (command == "Previous Step")
                {
                    return RedirectToAction("Edit", "Manage", new { articleid = articleVM.ArticleItem.ID });
                }
                else if (command == "Submit to Publish")
                {
                    datalayer = new DataAccessLayer();
                    if (datalayer.SubmitArticle(articleVM.ArticleItem, userprofile.ID, userprofile.Role))
                    {
                        return RedirectToAction("Index", "Manage");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error while publishing the article");
                        datalayer = new DataAccessLayer();
                        articleItem = datalayer.GetArticleEntryDetails(articleVM.ArticleItem.ID, userprofile.ID);
                        articleVM.ArticleItem = articleItem;

                        script = new StringBuilder();
                        script.Append("<script type='text/javascript'>");
                        script.Append("  $('#error_alert').fadeIn(350, function ());");
                        script.Append("</script>");

                        ViewBag.StartScript = script.ToString();

                        return View(articleVM);
                    }

                }
                else if (command == "Upload")
                {
                    if (uplimage == null)
                    {

                        ModelState.AddModelError("", "You didn't select an image file or the file you uploaded was invalid!");
                        datalayer = new DataAccessLayer();
                        articleItem = datalayer.GetArticleEntryDetails(articleVM.ArticleItem.ID, userprofile.ID);
                        articleVM.ArticleItem = articleItem;

                        script = new StringBuilder();
                        script.Append("<script type='text/javascript'>");
                        script.Append("  $('#error_alert').fadeIn(350, function () { $('#uplimage').focus(); });");
                        script.Append("</script>");

                        ViewBag.StartScript = script.ToString();

                        return View(articleVM);
                    }
                    else
                    {
                        var result = Helper_Magazine.UploadImage(uplimage, HttpContext.Server, articleVM.ArticleItem.ID,"entry");

                        if (!result.Errors.Any())
                        {
                            articleVM.ArticleItem.Image_path = result.ImagePath;
                            return RedirectToAction("ArticleOption", "Manage", new { articleid = articleVM.ArticleItem.ID.ToString() });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error while uploading image!");
                            articleVM.Errors = result.Errors;
                            datalayer = new DataAccessLayer();
                            articleItem = datalayer.GetArticleEntryDetails(articleVM.ArticleItem.ID, userprofile.ID);
                            articleVM.ArticleItem = articleItem;

                            script = new StringBuilder();
                            script.Append("<script type='text/javascript'>");
                            script.Append("  $('#error_alert').fadeIn(350, function () { $('#uplimage').focus(); });");
                            script.Append("</script>");

                            ViewBag.StartScript = script.ToString();

                            return View(articleVM);
                        }


                    }
                }
                else if (command == "Delete")
                {
                    var result = Helper_Magazine.DeleteImage(HttpContext.Server, articleVM.ArticleItem.ID,"entry");
                    if (!result.Errors.Any())
                    {
                        return RedirectToAction("ArticleOption", "Manage", new { articleid = articleVM.ArticleItem.ID.ToString() });
                    }
                    else
                    {
                        //some error occured while deleting the image
                        ModelState.AddModelError("", "Error while deleting image!");
                        articleVM.Errors = result.Errors;

                        script = new StringBuilder();
                        script.Append("<script type='text/javascript'>");
                        script.Append("  $('#error_alert').fadeIn(350, function () { $('#uplimage').focus(); });");
                        script.Append("</script>");

                        ViewBag.StartScript = script.ToString();

                        return View(articleVM);
                    }
                }

            }
            else
            {
                return RedirectToAction("Authenticate", "Account");

            }


            return View(articleVM);

        }

        public ActionResult ArticlePublishedOption(int articleid)
        {
            ArticleViewModel articleVM = null;
            ArticleItem articleItem = null;
            DataAccessLayer datalayer = null;
            List<TagItem> tags = null;

            //Guid userId;

            articleVM = new ArticleViewModel();

            if (Session["UserProfile"] != null)
            {
                UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                //articleVM.Userprofile = userprofile;
                //userId = userprofile.ID;

                datalayer = new DataAccessLayer();
                articleItem = datalayer.GetArticlePublishedDetails(articleid, userprofile.ID);

                if (articleItem != null)
                {

                    articleVM.ArticleItem = articleItem;

                    if (!string.IsNullOrEmpty(articleItem.Tag_Article))
                    {

                        tags = new List<TagItem>();
                        var tagSplit = articleItem.Tag_Article.Split(',');
                        for (var i = 0; i < tagSplit.Length - 1; i++)
                        {
                            TagItem tagitem = new TagItem { id = i, name = tagSplit[i] };
                            tags.Add(tagitem);
                        }
                        articleVM.ArticleItem.Tags = tags;
                    }

                }
                else
                {
                    articleVM = new ArticleViewModel();
                    articleVM.ArticleItem = new ArticleItem();
                    ModelState.AddModelError("", "Article Not Exist or Some error occured while fetching article details.");
                    StringBuilder script = new StringBuilder();
                    script.Append("<script type='text/javascript'>");
                    script.Append("$('#error_alert').show();");
                    script.Append("</script>");
                    ViewBag.StartScript = script.ToString();
                    return View(articleVM);
                }
            }
            else
            {
                return RedirectToAction("Authenticate", "Account");
            }


            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticlePublishedOption(ArticleViewModel articleVM, FormCollection formValues, string command, HttpPostedFileBase uplimage)
        {
            DataAccessLayer datalayer = null;
            ArticleItem articleItem = null;
            Guid valid_Articleid = Guid.Empty;
            StringBuilder script = null;
            //Guid userId;

            if (Session["UserProfile"] != null)
            {
                UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                // userId = userprofile.ID;

                if (command == "Previous Step")
                {
                    return RedirectToAction("EditPublished", "Manage", new { articleid = articleVM.ArticleItem.ID });
                }
                else if (command == "Publish")
                {
                    datalayer = new DataAccessLayer();
                    if (datalayer.SubmitPublishedArticle(articleVM.ArticleItem, userprofile.ID, userprofile.Role))
                    {
                        return RedirectToAction("Index", "Manage");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error while publishing the article");
                        datalayer = new DataAccessLayer();
                        articleItem = datalayer.GetArticlePublishedDetails(articleVM.ArticleItem.ID, userprofile.ID);
                        articleVM.ArticleItem = articleItem;

                        script = new StringBuilder();
                        script.Append("<script type='text/javascript'>");
                        script.Append("  $('#error_alert').fadeIn(350, function ());");
                        script.Append("</script>");

                        ViewBag.StartScript = script.ToString();

                        return View(articleVM);
                    }

                }
                else if (command == "Upload")
                {
                    if (uplimage == null)
                    {

                        ModelState.AddModelError("", "You didn't select an image file or the file you uploaded was invalid!");
                        datalayer = new DataAccessLayer();
                        articleItem = datalayer.GetArticlePublishedDetails(articleVM.ArticleItem.ID, userprofile.ID);
                        articleVM.ArticleItem = articleItem;

                        script = new StringBuilder();
                        script.Append("<script type='text/javascript'>");
                        script.Append("  $('#error_alert').fadeIn(350, function () { $('#uplimage').focus(); });");
                        script.Append("</script>");

                        ViewBag.StartScript = script.ToString();

                        return View(articleVM);
                    }
                    else
                    {
                        var result = Helper_Magazine.UploadImage(uplimage, HttpContext.Server, articleVM.ArticleItem.ID, "published");

                        if (!result.Errors.Any())
                        {
                            articleVM.ArticleItem.Image_path = result.ImagePath;
                            return RedirectToAction("ArticlePublishedOption", "Manage", new { articleid = articleVM.ArticleItem.ID.ToString() });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error while uploading image!");
                            articleVM.Errors = result.Errors;
                            datalayer = new DataAccessLayer();
                            articleItem = datalayer.GetArticlePublishedDetails(articleVM.ArticleItem.ID, userprofile.ID);
                            articleVM.ArticleItem = articleItem;

                            script = new StringBuilder();
                            script.Append("<script type='text/javascript'>");
                            script.Append("  $('#error_alert').fadeIn(350, function () { $('#uplimage').focus(); });");
                            script.Append("</script>");

                            ViewBag.StartScript = script.ToString();

                            return View(articleVM);
                        }


                    }
                }
                else if (command == "Delete")
                {
                    var result = Helper_Magazine.DeleteImage(HttpContext.Server, articleVM.ArticleItem.ID,"published");
                    if (!result.Errors.Any())
                    {
                        return RedirectToAction("ArticlePublishedOption", "Manage", new { articleid = articleVM.ArticleItem.ID.ToString() });
                    }
                    else
                    {
                        //some error occured while deleting the image
                        ModelState.AddModelError("", "Error while deleting image!");
                        articleVM.Errors = result.Errors;

                        script = new StringBuilder();
                        script.Append("<script type='text/javascript'>");
                        script.Append("  $('#error_alert').fadeIn(350, function () { $('#uplimage').focus(); });");
                        script.Append("</script>");

                        ViewBag.StartScript = script.ToString();

                        return View(articleVM);
                    }
                }

            }
            else
            {
                return RedirectToAction("Authenticate", "Account");

            }


            return View(articleVM);

        }

        public ActionResult Edit(int articleid)
        {
            ArticleViewModel ArticleVM = null;
            DataAccessLayer datalayer = null;
            ArticleItem articleItem = null;
            List<CategoryItem> article_categories = null;

            Guid userId;

            if (Session["UserProfile"] != null)
            {
                UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                userId = userprofile.ID;

                datalayer = new DataAccessLayer();
                articleItem = datalayer.GetArticleEntryDetails(articleid, userId);

                if (articleItem != null)
                {
                    ArticleVM = new ArticleViewModel();
                    ArticleVM.ArticleItem = articleItem;

                    article_categories = datalayer.GetCategories();
                    ArticleVM.Categories = article_categories;
                    string newtagstr = string.Empty;
                    if (!string.IsNullOrEmpty(articleItem.Tag_Article))
                    {
                        var tagSplit = articleItem.Tag_Article.Split(',');
                        newtagstr = "[";
                        for (var i = 0; i < tagSplit.Length - 1; i++)
                        {

                            newtagstr = newtagstr + "'" + tagSplit[i] + "',";

                        }
                        newtagstr = newtagstr.TrimEnd(',') + "]";

                    }
                    //ArticleVM.ArticleItem.Tag_Article = newtagstr;

                    ViewBag.HdnSelectedTags = newtagstr;

                    StringBuilder script = new StringBuilder();
                    script.Append("<script type='text/javascript'>");
                    script.Append("$('#" + articleItem.Category.Id + "').prop('checked', true);");
                    script.Append("</script>");
                    ViewBag.StartScript = script.ToString();
                    return View(ArticleVM);
                }
                else
                {
                    ArticleVM = new ArticleViewModel();
                    ArticleVM.ArticleItem = new ArticleItem();
                    ModelState.AddModelError("", "Article Not Exist or Some error occured while fetching article details.");
                    StringBuilder script = new StringBuilder();
                    script.Append("<script type='text/javascript'>");
                    script.Append("$('#error_alert').show();");
                    script.Append("</script>");
                    ViewBag.StartScript = script.ToString();
                    return View(ArticleVM);
                }

            }
            else
            {
                return RedirectToAction("Authenticate", "Account");
            }


        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleViewModel articleVM, FormCollection formValues, string command)
        {
            if (command == "Save & Preview")
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserProfile"] != null)
                    {
                        List<TagItem> tags = null;

                        UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                        articleVM.ArticleItem.Userid = userprofile.ID;
                        string articleID = articleVM.ArticleItem.ID.ToString();
                        if (!string.IsNullOrEmpty(articleID))
                        {
                            if (!string.IsNullOrEmpty(articleVM.ArticleItem.Content))
                            {
                                articleVM.ArticleItem.Content_summary = StringHtmlExtensions.TruncateHtml(articleVM.ArticleItem.Content, CONTENT_SUMMARY_LENGTH, "....");
                            }

                            if (!string.IsNullOrEmpty(articleVM.ArticleItem.Title_English))
                            {
                                articleVM.ArticleItem.Title_SEO = articleVM.ArticleItem.Title_English.ToSeoUrl();
                            }


                            //update the article entry
                            DataAccessLayer datalayer = null;
                            datalayer = new DataAccessLayer();
                            if (datalayer.UpdateArticleEntry(articleVM.ArticleItem))
                            {
                                if (!string.IsNullOrEmpty(articleVM.ArticleItem.Tag_Article))
                                {
                                    //update tag table for this article
                                    tags = new List<TagItem>();
                                    var tagSplit = articleVM.ArticleItem.Tag_Article.Split(',');
                                    for (var i = 0; i < tagSplit.Length - 1; i++)
                                    {
                                        TagItem tagitem = new TagItem { id = i, name = tagSplit[i] };
                                        tags.Add(tagitem);
                                    }
                                    bool istagsupdated = datalayer.UpdateTagEntry(tags, articleVM.ArticleItem.ID);
                                    if (!istagsupdated)
                                    {
                                        //log error that tag updation failed
                                    }

                                }

                                return RedirectToAction("ArticleOption", "Manage", new { articleid = articleID });
                            }
                            else
                            {
                                //some error occured in updation - redirect to error page
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Authenticate", "Account");
                    }
                }

            }
            else if (command == "Cancel")
            {
                return RedirectToAction("Index", "Manage");
            }
            return View(articleVM);
        }

        public ActionResult EditPublished(int articleid)
        {
            ArticleViewModel ArticleVM = null;
            DataAccessLayer datalayer = null;
            ArticleItem articleItem = null;
            List<CategoryItem> article_categories = null;

            Guid userId;

            if (Session["UserProfile"] != null)
            {
                UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                userId = userprofile.ID;

                datalayer = new DataAccessLayer();
                articleItem = datalayer.GetArticlePublishedDetails(articleid, userId);

                if (articleItem != null)
                {
                    ArticleVM = new ArticleViewModel();
                    ArticleVM.ArticleItem = articleItem;

                    article_categories = datalayer.GetCategories();
                    ArticleVM.Categories = article_categories;
                    string newtagstr = string.Empty;
                    if (!string.IsNullOrEmpty(articleItem.Tag_Article))
                    {
                        var tagSplit = articleItem.Tag_Article.Split(',');
                        newtagstr = "[";
                        for (var i = 0; i < tagSplit.Length - 1; i++)
                        {

                            newtagstr = newtagstr + "'" + tagSplit[i] + "',";

                        }
                        newtagstr = newtagstr.TrimEnd(',') + "]";

                    }
                    //ArticleVM.ArticleItem.Tag_Article = newtagstr;

                    ViewBag.HdnSelectedTags = newtagstr;

                    StringBuilder script = new StringBuilder();
                    script.Append("<script type='text/javascript'>");
                    script.Append("$('#" + articleItem.Category.Id + "').prop('checked', true);");
                    script.Append("</script>");
                    ViewBag.StartScript = script.ToString();
                    return View(ArticleVM);
                }
                else
                {
                    ArticleVM = new ArticleViewModel();
                    ArticleVM.ArticleItem = new ArticleItem();
                    ModelState.AddModelError("", "Article Not Exist or Some error occured while fetching article details.");
                    StringBuilder script = new StringBuilder();
                    script.Append("<script type='text/javascript'>");
                    script.Append("$('#error_alert').show();");
                    script.Append("</script>");
                    ViewBag.StartScript = script.ToString();
                    return View(ArticleVM);
                }

            }
            else
            {
                return RedirectToAction("Authenticate", "Account");
            }


        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditPublished(ArticleViewModel articleVM, FormCollection formValues, string command)
        {
            DataAccessLayer datalayer = null;
            List<CategoryItem> article_categories = null;

            if (command == "Save & Preview")
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserProfile"] != null)
                    {
                        List<TagItem> tags = null;
                        
                        UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                        articleVM.ArticleItem.Userid = userprofile.ID;
                        string articleID = articleVM.ArticleItem.ID.ToString();
                        if (!string.IsNullOrEmpty(articleID))
                        {
                            if (!string.IsNullOrEmpty(articleVM.ArticleItem.Content))
                            {
                                articleVM.ArticleItem.Content_summary = StringHtmlExtensions.TruncateHtml(articleVM.ArticleItem.Content, CONTENT_SUMMARY_LENGTH, "....");
                            }

                            if (!string.IsNullOrEmpty(articleVM.ArticleItem.Title_English))
                            {
                                articleVM.ArticleItem.Title_SEO = articleVM.ArticleItem.Title_English.ToSeoUrl();
                            }


                            //update the article entry
                            
                            datalayer = new DataAccessLayer();
                            if (datalayer.UpdateArticlePublished(articleVM.ArticleItem))
                            {
                                if (!string.IsNullOrEmpty(articleVM.ArticleItem.Tag_Article))
                                {
                                    //update tag table for this article
                                    tags = new List<TagItem>();
                                    var tagSplit = articleVM.ArticleItem.Tag_Article.Split(',');
                                    for (var i = 0; i < tagSplit.Length - 1; i++)
                                    {
                                        TagItem tagitem = new TagItem { id = i, name = tagSplit[i] };
                                        tags.Add(tagitem);
                                    }
                                    bool istagsupdated = datalayer.UpdateTagEntry(tags, articleVM.ArticleItem.ID);
                                    if (!istagsupdated)
                                    {
                                        //log error that tag updation failed
                                    }

                                }

                                return RedirectToAction("ArticlePublishedOption", "Manage", new { articleid = articleID });
                            }
                            else
                            {
                                //some error occured in updation - redirect to error page
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Authenticate", "Account");
                    }
                }

            }
            else if (command == "Cancel")
            {
                return RedirectToAction("Index", "Manage");
            }
            //TODO: Lot of work need to be done here for error handling, tags etc
            datalayer = new DataAccessLayer();
            article_categories = datalayer.GetCategories();
            articleVM.Categories = article_categories;
            StringBuilder script = new StringBuilder();
            script.Append("<script type='text/javascript'>");
            script.Append("$('#" + articleVM.ArticleItem.Category.Id + "').prop('checked', true);");
            //script.Append("$('#error_alert').show();");
            script.Append("</script>");
            ViewBag.StartScript = script.ToString();

            return View(articleVM);
        }

        public ActionResult Delete(int articleid)
        {
            DataAccessLayer datalayer = null;
            datalayer = new DataAccessLayer();
            bool isDeleted = datalayer.DeleteArticleEntry(articleid);
            if (!isDeleted)
            {
                //some error occured while article deletion
                //rediredct to error page
            }
            return RedirectToAction("Index", "Manage");

        }

        public ActionResult Approve(int articleid)
        {
            DataAccessLayer datalayer = null;

            if (Session["UserProfile"] != null)
            {
                UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                datalayer = new DataAccessLayer();
                bool isApproved = datalayer.ApproveArticle(articleid, userprofile.ID, userprofile.Role);
                if (!isApproved)
                {
                    //some error occured while article approval
                    //rediredct to error page
                }
                return RedirectToAction("Index", "Manage");
            }
            else
            {
                return RedirectToAction("Authenticate", "Account");
            }

        }

        public ActionResult Reject(int articleid)
        {
            DataAccessLayer datalayer = null;

            if (Session["UserProfile"] != null)
            {
                UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                datalayer = new DataAccessLayer();
                bool isRejected = datalayer.RejectArticle(articleid, userprofile.ID, userprofile.Role);
                if (!isRejected)
                {
                    //some error occured while article approval
                    //rediredct to error page
                }
                return RedirectToAction("Index", "Manage");
            }
            else
            {
                return RedirectToAction("Authenticate", "Account");
            }

        }

        public ActionResult Archive(int articleid)
        {
            DataAccessLayer datalayer = null;

            if (Session["UserProfile"] != null)
            {
                UserProfileItem userprofile = (UserProfileItem)Session["UserProfile"];
                datalayer = new DataAccessLayer();
                bool isArchived = datalayer.ArchiveArticle(articleid, userprofile.ID, userprofile.Role);
                if (!isArchived)
                {
                    //some error occured while article archival
                    //rediredct to error page
                }
                return RedirectToAction("Index", "Manage");
            }
            else
            {
                return RedirectToAction("Authenticate", "Account");
            }

        }

        public ActionResult DeleteArchive(int articleid)
        {
            DataAccessLayer datalayer = null;
            datalayer = new DataAccessLayer();
            bool isDeleted = datalayer.DeleteArticleArchived(articleid);
            if (!isDeleted)
            {
                //some error occured while archive article deletion
                //rediredct to error page
            }
            return RedirectToAction("Index", "Manage");

        }

        public ActionResult PreviewArticleEntry(string articleid)
        {            
            DataAccessLayer datalayer = null;
            ArticleItem articleItem = null;
            PostViewModel PostVM = null;
            List<TagItem> tags = null;
            int articleID_int;

            if (int.TryParse(articleid, out articleID_int) && articleID_int >= 1)
            {
                datalayer = new DataAccessLayer();
                articleItem = datalayer.GetArticleEntryDetails(articleID_int);

                if (articleItem != null)
                {
                    PostVM = new PostViewModel();
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

                }
            }
            return PartialView(PostVM);
        }

        public ActionResult PreviewArticlePublished(string articleid)
        {
            DataAccessLayer datalayer = null;
            ArticleItem articleItem = null;
            PostViewModel PostVM = null;
            List<TagItem> tags = null;
            int articleID_int;

            if (int.TryParse(articleid, out articleID_int) && articleID_int >= 1)
            {
                datalayer = new DataAccessLayer();
                articleItem = datalayer.GetArticlePublishedDetails(articleID_int);

                if (articleItem != null)
                {
                    PostVM = new PostViewModel();
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

                }
            }
            return PartialView(PostVM);
        }

        public ActionResult PreviewArticleArchived(string articleid)
        {
            DataAccessLayer datalayer = null;
            ArticleItem articleItem = null;
            PostViewModel PostVM = null;
            List<TagItem> tags = null;
            int articleID_int;

            if (int.TryParse(articleid, out articleID_int) && articleID_int >= 1)
            {
                datalayer = new DataAccessLayer();
                articleItem = datalayer.GetArticleArchivedDetails(articleID_int);

                if (articleItem != null)
                {
                    PostVM = new PostViewModel();
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

                }
            }
            return PartialView(PostVM);
        }
        
        //[HttpPost]
        //public JsonResult GetManagementData( string select)
        //{
        //    DataAccessLayer datalayer = null;
        //    List<CategoryItem> article_categories = null;
        //    List<UserProfileItem> userProfiles = null;
        //    List<NewsItem> breakingNewsItems = null;
        //    List<LatestNewsItem> latestTenNews = null;
        //    BreakingNews breakingNews = null;

        //    JavaScriptSerializer jserializer = null;
        //    string json = string.Empty;

        //    if(!string.IsNullOrEmpty(select)){

        //        if (select == "categories") {

        //            // 1-  get all catagories

        //            datalayer = new DataAccessLayer();
        //            article_categories = datalayer.GetCategories();
        //            jserializer = new JavaScriptSerializer();
        //            json = jserializer.Serialize(article_categories);
        //        }

        //        else if(select =="profiles"){
        //            // 2 - get all user profiles
        //            datalayer = new DataAccessLayer();
        //            userProfiles = datalayer.GetAllUsers();
        //            jserializer = new JavaScriptSerializer();
        //            json = jserializer.Serialize(userProfiles);
        //        }

        //        else if (select == "breaking")
        //        {

        //            breakingNewsItems = datalayer.GetBreakingNews();

        //            //bind drop down with top 10 latest news
        //            latestTenNews = datalayer.GetLatestTenNews();

        //            foreach (NewsItem newsitem in breakingNewsItems)
        //            {
        //                newsitem.TopNewsDdl = new List<SelectListItem>();
        //                foreach (LatestNewsItem item in latestTenNews)
        //                {
        //                    SelectListItem selectListItem = new SelectListItem { Text = item.title, Value = item.id.ToString(), Selected = item.id == newsitem.Article_id };
        //                    newsitem.TopNewsDdl.Add(selectListItem);
        //                }
        //            }
        //            breakingNews = new BreakingNews { BreakingNewsItems = breakingNewsItems, LatestTenNews = latestTenNews };
        //            jserializer = new JavaScriptSerializer();
        //            json = jserializer.Serialize(breakingNews);
        //        }


        //    }


        //    //return Json(tags.Select(p => new { TagId = p.Id, TagName = p.Name }), JsonRequestBehavior.AllowGet);



        //    return Json(json, JsonRequestBehavior.AllowGet);

        //}

    }
}
