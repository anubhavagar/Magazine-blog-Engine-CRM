using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AapkiMagazine.Models;
using AapkiMagazine.Helper;
//using AapkiMagazine.DataEntity.Dev;
using AapkiMagazine.DataEntity.prod;
using System.Data.Objects;
using System.Transactions;

namespace AapkiMagazine.DataLayer
{
    public class DataAccessLayer : DataAccessBase
    {
        public List<CategoryItem> GetCategories()
        {
            List<CategoryItem> categories = null;
            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticleCategories.MergeOption = MergeOption.NoTracking;
                categories = (from a in AppKiMagazineDbContext.DbObjectContext.ArticleCategories

                              select new CategoryItem
                                {
                                    Id = a.id,
                                    Name = a.name
                                }
                        ).ToList();

            }
            catch (Exception ex)
            {
                // Throw Exception
                return categories;
            }

            return categories;
        }

        public List<UserProfileItem> GetAllUsers(string userRole)
        {
            List<UserProfileItem> userProfiles = null;
            try
            {
                AppKiMagazineDbContext.DbObjectContext.PortalUserProfiles.MergeOption = MergeOption.NoTracking;
                if (userRole == "sadmin")
                {
                    userProfiles = (from a in AppKiMagazineDbContext.DbObjectContext.PortalUserProfiles                                    
                                    select new UserProfileItem
                                    {
                                        ID = a.User_Id,
                                        First_Name = a.first_name,
                                        Last_Name = a.last_name,
                                        EmailId = a.user_email,
                                        Role = a.Role,
                                        CreatedDate = a.createddate

                                    }
                           ).OrderByDescending(e => e.CreatedDate).ToList();
                }
                else if (userRole == "admin")
                {
                    userProfiles = (from a in AppKiMagazineDbContext.DbObjectContext.PortalUserProfiles
                                    where (a.Role != "sadmin")
                                    select new UserProfileItem
                                    {
                                        ID = a.User_Id,
                                        First_Name = a.first_name,
                                        Last_Name = a.last_name,
                                        EmailId = a.user_email,
                                        Role = a.Role,
                                        CreatedDate = a.createddate

                                    }
                           ).OrderByDescending(e => e.CreatedDate).ToList();
                }
                else if (userRole == "ceditor")
                {
                    userProfiles = (from a in AppKiMagazineDbContext.DbObjectContext.PortalUserProfiles
                                    where (a.Role != "sadmin" && a.Role != "admin")
                                    select new UserProfileItem
                                    {
                                        ID = a.User_Id,
                                        First_Name = a.first_name,
                                        Last_Name = a.last_name,
                                        EmailId = a.user_email,
                                        Role = a.Role,
                                        CreatedDate = a.createddate

                                    }
                           ).OrderByDescending(e => e.CreatedDate).ToList();
                }



            }
            catch (Exception ex)
            {
                // Throw Exception
                return userProfiles;
            }

            return userProfiles;
        }

        public List<ArticleItem> GetAllArticlesEntry(Guid userid, string userRole)
        {
            List<ArticleItem> articleItems = null;
            try
            {
                if (userRole == "admin" || userRole == "ceditor"|| userRole == "sadmin" )
                {
                    articleItems = (from articleEntry in AppKiMagazineDbContext.DbObjectContext.ArticleEntries
                                    where ((articleEntry.PortalUserProfile.User_Id == userid) || (articleEntry.active == 1))
                                    select new ArticleItem
                                    {
                                        ID = articleEntry.id,
                                        Author = articleEntry.author,
                                        Company = articleEntry.company,
                                        Created_Date = articleEntry.Article.created_date,                                        
                                        Title = articleEntry.title,
                                        Category = new CategoryItem { Id = articleEntry.ArticleCategory.id, Name = articleEntry.ArticleCategory.name },                                        
                                        active = articleEntry.active,
                                        Image_path = articleEntry.Picture.picture_url,
                                        Userid = articleEntry.user_id,
                                        Video_url = articleEntry.Video.video_url,                                        
                                        Modified_Date = articleEntry.Article.modified_date ?? articleEntry.Article.created_date,
                                        EmailID = articleEntry.PortalUserProfile.user_email
                                    }).OrderByDescending(e => e.Modified_Date).ToList();
                }
                else if (userRole == "editor")
                {
                    articleItems = (from articleEntry in AppKiMagazineDbContext.DbObjectContext.ArticleEntries
                                    where (articleEntry.user_id == userid)
                                    select new ArticleItem
                                    {
                                        ID = articleEntry.id,
                                        Author = articleEntry.author,
                                        Userid = articleEntry.user_id,
                                        Company = articleEntry.company,
                                        Created_Date = articleEntry.Article.created_date,                                        
                                        Title = articleEntry.title,
                                        Category = new CategoryItem { Id = articleEntry.ArticleCategory.id, Name = articleEntry.ArticleCategory.name },
                                        active = articleEntry.active,                                        
                                        Image_path = articleEntry.Picture.picture_url,
                                        Video_url = articleEntry.Video.video_url,                                        
                                        Modified_Date = articleEntry.Article.modified_date ?? articleEntry.Article.created_date,
                                        EmailID = articleEntry.PortalUserProfile.user_email
                                    }).OrderByDescending(e => e.Modified_Date).ToList();
                }


                return articleItems;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return articleItems;
            }

        }

        public List<ArticleItem> GetAllArticlesPublished(Guid userid, string userRole)
        {
            List<ArticleItem> articleItems = null;
            try
            {
                if (userRole == "admin" || userRole == "ceditor" || userRole == "sadmin")
                {
                    articleItems = (from articlePublished in AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                                    select new ArticleItem
                                    {
                                        ID = articlePublished.id,
                                        Author = articlePublished.author,
                                        Company = articlePublished.company,
                                        Created_Date = articlePublished.Article.created_date,
                                        //Content = articleEntry.content_full,// Modified_Date = article.Article.modified_date,
                                        Title = articlePublished.title,
                                        Category = new CategoryItem { Id = articlePublished.ArticleCategory.id, Name = articlePublished.ArticleCategory.name },
                                        //Title_English = articleEntry.title_english,
                                        Image_path = articlePublished.Picture.picture_url,
                                        Userid = articlePublished.user_id,
                                        Video_url = articlePublished.Video.video_url,
                                        //published = articlePublished.published,
                                        Modified_Date = articlePublished.Article.modified_date ?? articlePublished.Article.created_date,
                                        EmailID = articlePublished.PortalUserProfile.user_email
                                    }).OrderByDescending(e => e.Modified_Date).ToList();
                }
                else if (userRole == "editor")
                {

                    articleItems = (from articlePublished in AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                                    where (articlePublished.user_id == userid)
                                    select new ArticleItem
                                    {
                                        ID = articlePublished.id,
                                        Author = articlePublished.author,
                                        Userid = articlePublished.user_id,
                                        Company = articlePublished.company,
                                        Created_Date = articlePublished.Article.created_date,
                                        //Content = articleEntry.content_full,// Modified_Date = article.Article.modified_date,
                                        Title = articlePublished.title,
                                        Category = new CategoryItem { Id = articlePublished.ArticleCategory.id, Name = articlePublished.ArticleCategory.name },
                                        //Title_English = articleEntry.title_english, 
                                        Image_path = articlePublished.Picture.picture_url,
                                        Video_url = articlePublished.Video.video_url,
                                        //published = articlePublished.published,
                                        Modified_Date = articlePublished.Article.modified_date ?? articlePublished.Article.created_date,
                                        EmailID = articlePublished.PortalUserProfile.user_email
                                    }).OrderByDescending(e => e.Modified_Date).ToList();
                }


                return articleItems;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return articleItems;
            }

        }

        public List<ArticleItem> GetAllArticlesArchived(Guid userid, string userRole)
        {
            List<ArticleItem> articleItems = null;
            try
            {
                if (userRole == "admin" || userRole == "ceditor" || userRole == "sadmin")
                {
                    articleItems = (from articleArchive in AppKiMagazineDbContext.DbObjectContext.ArticleArchiveds
                                    select new ArticleItem
                                    {
                                        ID = articleArchive.id,
                                        Author = articleArchive.author,
                                        Company = articleArchive.company,
                                        Created_Date = articleArchive.Article.created_date,
                                        //Content = articleEntry.content_full,// Modified_Date = article.Article.modified_date,
                                        Title = articleArchive.title,
                                        Category = new CategoryItem { Id = articleArchive.ArticleCategory.id, Name = articleArchive.ArticleCategory.name },
                                        //Title_English = articleEntry.title_english,
                                        Image_path = articleArchive.Picture.picture_url,
                                        Userid = articleArchive.user_id,
                                        Video_url = articleArchive.Video.video_url,
                                        //published = articleEntry.published,
                                        Modified_Date = articleArchive.Article.modified_date ?? articleArchive.Article.created_date,
                                        EmailID = articleArchive.PortalUserProfile.user_email
                                    }).OrderByDescending(e => e.Modified_Date).ToList();
                }
                else
                {

                    articleItems = (from articleArchive in AppKiMagazineDbContext.DbObjectContext.ArticleArchiveds
                                    where (articleArchive.user_id == userid)
                                    select new ArticleItem
                                    {
                                        ID = articleArchive.id,
                                        Author = articleArchive.author,
                                        Userid = articleArchive.user_id,
                                        Company = articleArchive.company,
                                        Created_Date = articleArchive.Article.created_date,
                                        //Content = articleEntry.content_full,// Modified_Date = article.Article.modified_date,
                                        Title = articleArchive.title,
                                        Category = new CategoryItem { Id = articleArchive.ArticleCategory.id, Name = articleArchive.ArticleCategory.name },
                                        //Title_English = articleEntry.title_english, 
                                        Image_path = articleArchive.Picture.picture_url,
                                        Video_url = articleArchive.Video.video_url,
                                        //published = articleEntry.published,
                                        Modified_Date = articleArchive.Article.modified_date ?? articleArchive.Article.created_date,
                                        EmailID = articleArchive.PortalUserProfile.user_email
                                    }).OrderByDescending(e => e.Modified_Date).ToList();
                }


                return articleItems;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return articleItems;
            }

        }

        public UserProfileItem ValidateAndGetUserProfile(string userEmail, string pwd)
        {
            PortalUserProfile userProfile = null;
            UserProfileItem userProfileItem = null;
            try
            {
                userProfile = AppKiMagazineDbContext.DbObjectContext.PortalUserProfiles.FirstOrDefault(i => i.user_email == userEmail);
                string hashdpwd = Helper_Magazine.CreatePasswordHash(pwd, userProfile.salt);
                if (userProfile != null)
                {
                    if (userProfile.Password == hashdpwd && (userProfile.Role != "newuser"))
                    {
                        userProfileItem = new UserProfileItem
                        {
                            ID = userProfile.User_Id,
                            First_Name = userProfile.first_name,
                            Last_Name = userProfile.last_name,
                            EmailId = userProfile.user_email,
                            Role = userProfile.Role,
                            Valid = true
                        };

                    }

                }

                return userProfileItem;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return userProfileItem;
            }

        }

        public List<NewsItem> GetBreakingNews()
        {
            List<NewsItem> latestNews = null;
            try
            {
                //selecting top 5 breaking news items
                AppKiMagazineDbContext.DbObjectContext.BreakingNews.MergeOption = MergeOption.NoTracking;
                latestNews = (from breakingNews in AppKiMagazineDbContext.DbObjectContext.BreakingNews
                              select new NewsItem
                              {
                                  Id = breakingNews.id,
                                  CustomText = breakingNews.custom_text,
                                  Article_id = breakingNews.ArticlePublished.id,
                                  Title_SEO = breakingNews.ArticlePublished.title_seo,
                                  Title = breakingNews.ArticlePublished.title
                              }
                        ).Take(5).ToList();

            }
            catch (Exception ex)
            {
                // Throw Exception

                return latestNews;
            }

            return latestNews;
        }

        public List<LatestNewsItem> GetLatestTenNews()
        {
            List<LatestNewsItem> topnews = null;

            try
            {
                topnews = AppKiMagazineDbContext.DbObjectContext.GetLatestTenNews().ToList();

            }
            catch (Exception ex)
            {
                // Throw Exception
                return topnews;
            }
            return topnews;


        }

        public bool UserEmailExist(string userEmail)
        {
            PortalUserProfile userProfile = null;

            try
            {
                userProfile = AppKiMagazineDbContext.DbObjectContext.PortalUserProfiles.FirstOrDefault(i => i.user_email == userEmail);
                //string hashdpwd = Helper_Magazine.CreatePasswordHash(pwd, userProfile.salt);
                if (userProfile == null)
                {

                    return false;

                }
                else { return true; }

            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }

        }

        public bool CreateNewUser(string userEmail, string password, string firstName, string lastName)
        {
            string salt = Helper_Magazine.CreateSalt(8);
            string hashdpwd = Helper_Magazine.CreatePasswordHash(password, salt);
            Guid userid = Guid.NewGuid();
            PortalUserProfile newUser = new PortalUserProfile
            {
                User_Id = userid,
                user_email = userEmail,
                first_name = firstName,
                last_name = lastName,
                salt = salt,
                Role = "newuser",
                Password = hashdpwd, 
                createddate = DateTime.Now
            };

            try
            {
                AppKiMagazineDbContext.DbObjectContext.PortalUserProfiles.AddObject(newUser);
                AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }

        }

        public bool ResetPassword(string userEmail, string newpassword)
        {
            string newsalt = Helper_Magazine.CreateSalt(8);
            string newhashdpwd = Helper_Magazine.CreatePasswordHash(newpassword, newsalt);
            PortalUserProfile userprofile = null;
            try
            {
                userprofile = AppKiMagazineDbContext.DbObjectContext.PortalUserProfiles.FirstOrDefault(i => i.user_email == userEmail);
                if (userprofile != null)
                {
                    userprofile.Password = newhashdpwd;
                    userprofile.salt = newsalt;
                    userprofile.pwdchangedate = DateTime.Now;
                    AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                }
                else
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }



        }

        public int CreateArticle(ArticleItem articleItem)
        {            
            articleItem.Created_Date = DateTime.Now;
            articleItem.Modified_Date = DateTime.Now;

            Article newArticle = new Article
           {               
               created_date = articleItem.Created_Date,
               modified_date = articleItem.Modified_Date
           };

            ArticleEntry newarticleEntry = new ArticleEntry()
            {
                id = newArticle.id,                
                category_id = articleItem.Category.Id,
                author = articleItem.Author,
                user_id = articleItem.Userid,
                company = articleItem.Company,
                content_full = articleItem.Content,
                content_summary = articleItem.Content_summary,
                title = articleItem.Title,
                title_english = articleItem.Title_English,
                title_seo = articleItem.Title_SEO,
                tags = articleItem.Tag_Article,                
                Article = newArticle
            };

            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticleEntries.AddObject(newarticleEntry);
                AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                return newarticleEntry.id;

            }
            catch (Exception ex)
            {
                // Throw Exception
                return 0;
            }

        }

        public ArticleItem GetArticleEntryDetails(int articleID, Guid userId)
        {
            ArticleItem articleItem = new ArticleItem();
            //Guid g = new Guid(articleID);
            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticleEntries.MergeOption = MergeOption.NoTracking;
                articleItem = (from articleEntry in AppKiMagazineDbContext.DbObjectContext.ArticleEntries
                               where (articleEntry.id == articleID)
                               select new ArticleItem
                               {
                                   ID = articleEntry.id,
                                   Author = articleEntry.author,
                                   Userid = articleEntry.user_id,
                                   Company = articleEntry.company,
                                   Created_Date = articleEntry.Article.created_date,
                                   Content = articleEntry.content_full,// Modified_Date = article.Article.modified_date,
                                   Title = articleEntry.title,
                                   Category = new CategoryItem { Id = articleEntry.ArticleCategory.id, Name = articleEntry.ArticleCategory.name },
                                   Title_English = articleEntry.title_english,
                                   Image_path = articleEntry.Picture.picture_url,
                                   Video_url = articleEntry.Video.video_url,
                                   //published = articleEntry.published,
                                   Tag_Article = articleEntry.tags,
                                   Modified_Date = articleEntry.Article.modified_date ?? articleEntry.Article.created_date
                               }).FirstOrDefault();
                return articleItem;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return articleItem;
            }

        }

        public ArticleItem GetArticlePublishedDetails(int articleID, Guid userId)
        {
            ArticleItem articleItem = new ArticleItem();
            //Guid g = new Guid(articleID);
            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticleEntries.MergeOption = MergeOption.NoTracking;
                articleItem = (from articlePublished in AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                               where (articlePublished.id == articleID)
                               select new ArticleItem
                               {
                                   ID = articlePublished.id,
                                   Author = articlePublished.author,
                                   Userid = articlePublished.user_id,
                                   Company = articlePublished.company,
                                   Created_Date = articlePublished.Article.created_date,
                                   Content = articlePublished.content_full,// Modified_Date = article.Article.modified_date,
                                   Title = articlePublished.title,
                                   Category = new CategoryItem { Id = articlePublished.ArticleCategory.id, Name = articlePublished.ArticleCategory.name },
                                   Title_English = articlePublished.title_english,
                                   Image_path = articlePublished.Picture.picture_url,
                                   Video_url = articlePublished.Video.video_url,
                                   //published = articleEntry.published,
                                   Tag_Article = articlePublished.tags,
                                   Modified_Date = articlePublished.Article.modified_date ?? articlePublished.Article.created_date
                               }).FirstOrDefault();
                return articleItem;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return articleItem;
            }

        }

        public ArticleItem GetArticleDetails(int articleID)
        {
            ArticleItem articleItem = new ArticleItem();
            try
            {
                // AppKiMagazineDbContext.DbObjectContext.ArticleEntries.MergeOption = MergeOption.NoTracking; 
                articleItem = (from articleEntry in AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                               where (articleEntry.id == articleID)
                               select new ArticleItem
                               {
                                   ID = articleEntry.id,
                                   Author = articleEntry.author,
                                   //EmailID = articleEntry.user_emailid,
                                   Company = articleEntry.company,
                                   Created_Date = articleEntry.Article.created_date,
                                   Content = articleEntry.content_full,// Modified_Date = article.Article.modified_date,
                                   Title = articleEntry.title,
                                   Title_SEO = articleEntry.title_seo,
                                   Category = new CategoryItem { Id = articleEntry.ArticleCategory.id, Name = articleEntry.ArticleCategory.name, Name_english = articleEntry.ArticleCategory.name_english },
                                   Title_English = articleEntry.title_english,
                                   Image_path = articleEntry.Picture.picture_url,
                                   Video_url = articleEntry.Video.video_url,
                                   Tag_Article = articleEntry.tags,
                                   //published = articleEntry.published,
                                   Modified_Date = articleEntry.Article.modified_date ?? articleEntry.Article.created_date
                               }).FirstOrDefault();
                return articleItem;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return articleItem;
            }

        }

        public ArticleItem GetArticleEntryDetails(int articleID)
        {
            ArticleItem articleItem = new ArticleItem();
            try
            {
                // AppKiMagazineDbContext.DbObjectContext.ArticleEntries.MergeOption = MergeOption.NoTracking; 
                articleItem = (from articleEntry in AppKiMagazineDbContext.DbObjectContext.ArticleEntries
                               where (articleEntry.id == articleID)
                               select new ArticleItem
                               {
                                   ID = articleEntry.id,
                                   Author = articleEntry.author,
                                   //EmailID = articleEntry.user_emailid,
                                   Company = articleEntry.company,
                                   Created_Date = articleEntry.Article.created_date,
                                   Content = articleEntry.content_full,// Modified_Date = article.Article.modified_date,
                                   Title = articleEntry.title,
                                   Title_SEO = articleEntry.title_seo,
                                   Category = new CategoryItem { Id = articleEntry.ArticleCategory.id, Name = articleEntry.ArticleCategory.name, Name_english = articleEntry.ArticleCategory.name_english },
                                   Title_English = articleEntry.title_english,
                                   Image_path = articleEntry.Picture.picture_url,
                                   Video_url = articleEntry.Video.video_url,
                                   Tag_Article = articleEntry.tags,
                                   //published = articleEntry.published,
                                   Modified_Date = articleEntry.Article.modified_date ?? articleEntry.Article.created_date
                               }).FirstOrDefault();
                return articleItem;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return articleItem;
            }

        }

        public ArticleItem GetArticlePublishedDetails(int articleID)
        {
            ArticleItem articleItem = new ArticleItem();
            try
            {
                // AppKiMagazineDbContext.DbObjectContext.ArticleEntries.MergeOption = MergeOption.NoTracking; 
                articleItem = (from articlePublished in AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                               where (articlePublished.id == articleID)
                               select new ArticleItem
                               {
                                   ID = articlePublished.id,
                                   Author = articlePublished.author,
                                   //EmailID = articleEntry.user_emailid,
                                   Company = articlePublished.company,
                                   Created_Date = articlePublished.Article.created_date,
                                   Content = articlePublished.content_full,// Modified_Date = article.Article.modified_date,
                                   Title = articlePublished.title,
                                   Title_SEO = articlePublished.title_seo,
                                   Category = new CategoryItem { Id = articlePublished.ArticleCategory.id, Name = articlePublished.ArticleCategory.name, Name_english = articlePublished.ArticleCategory.name_english },
                                   Title_English = articlePublished.title_english,
                                   Image_path = articlePublished.Picture.picture_url,
                                   Video_url = articlePublished.Video.video_url,
                                   Tag_Article = articlePublished.tags,
                                   //published = articleEntry.published,
                                   Modified_Date = articlePublished.Article.modified_date ?? articlePublished.Article.created_date
                               }).FirstOrDefault();
                return articleItem;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return articleItem;
            }

        }

        public ArticleItem GetArticleArchivedDetails(int articleID)
        {
            ArticleItem articleItem = new ArticleItem();
            try
            {
                // AppKiMagazineDbContext.DbObjectContext.ArticleEntries.MergeOption = MergeOption.NoTracking; 
                articleItem = (from articleArchived in AppKiMagazineDbContext.DbObjectContext.ArticleArchiveds
                               where (articleArchived.id == articleID)
                               select new ArticleItem
                               {
                                   ID = articleArchived.id,
                                   Author = articleArchived.author,
                                   //EmailID = articleEntry.user_emailid,
                                   Company = articleArchived.company,
                                   Created_Date = articleArchived.Article.created_date,
                                   Content = articleArchived.content_full,// Modified_Date = article.Article.modified_date,
                                   Title = articleArchived.title,
                                   Title_SEO = articleArchived.title_seo,
                                   Category = new CategoryItem { Id = articleArchived.ArticleCategory.id, Name = articleArchived.ArticleCategory.name, Name_english = articleArchived.ArticleCategory.name_english },
                                   Title_English = articleArchived.title_english,
                                   Image_path = articleArchived.Picture.picture_url,
                                   Video_url = articleArchived.Video.video_url,
                                   Tag_Article = articleArchived.tags,
                                   //published = articleEntry.published,
                                   Modified_Date = articleArchived.Article.modified_date ?? articleArchived.Article.created_date
                               }).FirstOrDefault();
                return articleItem;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return articleItem;
            }

        }

        public List<ArticleItem> GetArticlesByCategory(string categoryType)
        {

            List<ArticleItem> articleItems = null;

            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.MergeOption = MergeOption.NoTracking;
                articleItems = (from articlePublished in AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                                where (articlePublished.ArticleCategory.name_english == categoryType)
                                select new ArticleItem
                                {
                                    ID = articlePublished.id,
                                    Author = articlePublished.author,
                                    Company = articlePublished.company,
                                    Created_Date = articlePublished.Article.created_date,
                                    Content = articlePublished.content_full,
                                    Content_summary = articlePublished.content_summary,
                                    Title = articlePublished.title,
                                    Category = new CategoryItem { Id = articlePublished.ArticleCategory.id, Name = articlePublished.ArticleCategory.name, Name_english = articlePublished.ArticleCategory.name_english },
                                    Title_SEO = articlePublished.title_seo,                                    
                                    Image_path = articlePublished.Picture.picture_url,
                                    Video_url = articlePublished.Video.video_url, 
                                    Modified_Date = articlePublished.Article.modified_date                                    
                                }).OrderByDescending(e=>e.Modified_Date).ToList();

            }

            catch (Exception ex)
            {
                // Throw Exception
                return articleItems;
            }

            return articleItems;
        }

        public int CheckIfValidCategory(string categoryType)
        {

            int categoryId = 0;
            try
            {
                categoryId = (from category in AppKiMagazineDbContext.DbObjectContext.ArticleCategories
                              where (category.name_english == categoryType)
                              select category.id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Throw Exception
                return categoryId;
            }

            return categoryId;

        }

        public bool UpdateArticleEntry(ArticleItem articleItem)
        {            
            ArticleEntry articleEntry = null;
            try
            {
                articleEntry = AppKiMagazineDbContext.DbObjectContext.ArticleEntries.FirstOrDefault(i => i.id == articleItem.ID);
                if (articleEntry != null)
                {
                    if (articleEntry.title != articleItem.Title)
                    {
                        articleEntry.title = articleItem.Title;
                    }

                    if (articleEntry.title_english != articleItem.Title_English)
                    {
                        articleEntry.title_english = articleItem.Title_English;
                    }

                    if (articleEntry.title_seo != articleItem.Title_SEO)
                    {
                        articleEntry.title_seo = articleItem.Title_SEO;
                    }

                    if (articleEntry.content_full != articleItem.Content)
                    {
                        articleEntry.content_full = articleItem.Content;
                    }

                    if (articleEntry.category_id != articleItem.Category.Id)
                    {
                        articleEntry.category_id = articleItem.Category.Id;
                    }

                    if (articleEntry.author != articleItem.Author)
                    {
                        articleEntry.author = articleItem.Author;
                    }                   

                    if (articleEntry.company != articleItem.Company)
                    {
                        articleEntry.company = articleItem.Company;
                    }

                    if (articleEntry.content_summary != articleItem.Content_summary)
                    {
                        articleEntry.content_summary = articleItem.Content_summary;
                    }

                    if (articleEntry.tags != articleItem.Tag_Article)
                    {
                        articleEntry.tags = articleItem.Tag_Article;
                    }

                    articleEntry.Article.modified_date = DateTime.Now;

                    AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                }
                else
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }

        }

        public bool UpdateArticlePublished(ArticleItem articleItem)
        {            
            ArticlePublished articlePublished = null;
            try
            {
                articlePublished = AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.FirstOrDefault(i => i.id == articleItem.ID);
                if (articlePublished != null)
                {
                    if (articlePublished.title != articleItem.Title)
                    {
                        articlePublished.title = articleItem.Title;
                    }

                    if (articlePublished.title_english != articleItem.Title_English)
                    {
                        articlePublished.title_english = articleItem.Title_English;
                    }

                    if (articlePublished.title_seo != articleItem.Title_SEO)
                    {
                        articlePublished.title_seo = articleItem.Title_SEO;
                    }

                    if (articlePublished.content_full != articleItem.Content)
                    {
                        articlePublished.content_full = articleItem.Content;
                    }

                    if (articlePublished.category_id != articleItem.Category.Id)
                    {
                        articlePublished.category_id = articleItem.Category.Id;
                    }

                    if (articlePublished.author != articleItem.Author)
                    {
                        articlePublished.author = articleItem.Author;
                    }                   

                    if (articlePublished.company != articleItem.Company)
                    {
                        articlePublished.company = articleItem.Company;
                    }

                    if (articlePublished.content_summary != articleItem.Content_summary)
                    {
                        articlePublished.content_summary = articleItem.Content_summary;
                    }

                    if (articlePublished.tags != articleItem.Tag_Article)
                    {
                        articlePublished.tags = articleItem.Tag_Article;
                    }

                    articlePublished.Article.modified_date = DateTime.Now;

                    AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                }
                else
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }

        }

        public bool UpdateArticleEntryImage(int articleId, string imagePath)
        {
            ArticleEntry articleEntry = null;
            Picture picture = null;

            try
            {
                picture = AppKiMagazineDbContext.DbObjectContext.Pictures.FirstOrDefault(i => i.article_id == articleId);

                if (picture != null)
                {
                    picture.picture_url = imagePath;
                }
                else
                {
                    picture = new Picture { article_id = articleId, picture_url = imagePath };
                    AppKiMagazineDbContext.DbObjectContext.Pictures.AddObject(picture);
                    AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                }


                articleEntry = AppKiMagazineDbContext.DbObjectContext.ArticleEntries.FirstOrDefault(i => i.id == articleId);
                if (articleEntry != null)
                {
                    if (picture != null)
                    {
                        articleEntry.picture_id = picture.id;
                        AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        public bool UpdateArticlePublishedImage(int articleId, string imagePath)
        {
            ArticlePublished articlePublished = null;
            Picture picture = null;

            try
            {
                picture = AppKiMagazineDbContext.DbObjectContext.Pictures.FirstOrDefault(i => i.article_id == articleId);

                if (picture != null)
                {
                    picture.picture_url = imagePath;
                }
                else
                {
                    picture = new Picture { article_id = articleId, picture_url = imagePath };
                    AppKiMagazineDbContext.DbObjectContext.Pictures.AddObject(picture);
                    AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                }


                articlePublished = AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.FirstOrDefault(i => i.id == articleId);
                if (articlePublished != null)
                {
                    if (picture != null)
                    {
                        articlePublished.picture_id = picture.id;
                        AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        public bool SubmitArticle(ArticleItem articleItem, Guid userid, string userRole)
        {
            ArticleEntry articleEntry = null;
            Video video = null;

            try
            {
                video = AppKiMagazineDbContext.DbObjectContext.Videos.FirstOrDefault(i => i.article_id == articleItem.ID);
                if (video != null)
                {
                    if ((video.video_url != articleItem.Video_url) && (articleItem.validyturl))
                    {
                        video.video_url = articleItem.Video_url;
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(articleItem.Video_url) && (articleItem.validyturl))
                    {
                        video = new Video { article_id = articleItem.ID, video_url = articleItem.Video_url };
                        AppKiMagazineDbContext.DbObjectContext.Videos.AddObject(video);
                        AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                    }

                }
                articleEntry = AppKiMagazineDbContext.DbObjectContext.ArticleEntries.FirstOrDefault(i => i.id == articleItem.ID);
                if (articleEntry != null)
                {
                    if (video != null)
                    {
                        articleEntry.video_id = video.id;
                    }
                    articleEntry.active = 1;

                    if (userRole == "admin" || userRole == "ceditor" || userRole == "sadmin")
                    {
                       ArticlePublished articlepublished = new ArticlePublished
                            {
                                id = articleEntry.id,
                                active = articleEntry.active,
                                author = articleEntry.author,
                                category_id = articleEntry.category_id,
                                company = articleEntry.company,
                                content_full = articleEntry.content_full,
                                content_summary = articleEntry.content_summary,
                                isbreakingnews = articleEntry.isbreakingnews,
                                video_id = articleEntry.video_id,
                                user_id = articleEntry.user_id,
                                title_seo = articleEntry.title_seo,
                                title_english = articleEntry.title_english,
                                tags = articleEntry.tags,
                                title = articleEntry.title,                               
                                picture_id = articleEntry.picture_id,
                                approver_id = userid

                            };
                            AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.AddObject(articlepublished);
                            AppKiMagazineDbContext.DbObjectContext.ArticleEntries.DeleteObject(articleEntry);
                        
                    }

                    AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        public bool SubmitPublishedArticle(ArticleItem articleItem, Guid userid, string userRole)
        {
            ArticlePublished articleEntry = null;
            Article article = null;
            Video video = null;

            try
            {
                video = AppKiMagazineDbContext.DbObjectContext.Videos.FirstOrDefault(i => i.article_id == articleItem.ID);
                if (video != null)
                {
                    if ((video.video_url != articleItem.Video_url) && (articleItem.validyturl))
                    {
                        video.video_url = articleItem.Video_url;
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(articleItem.Video_url) && (articleItem.validyturl))
                    {
                        video = new Video { article_id = articleItem.ID, video_url = articleItem.Video_url };
                        AppKiMagazineDbContext.DbObjectContext.Videos.AddObject(video);
                        AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                    }

                }
                articleEntry = AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.FirstOrDefault(i => i.id == articleItem.ID);
                article = AppKiMagazineDbContext.DbObjectContext.Articles.FirstOrDefault(i => i.id == articleItem.ID);
                if (articleEntry != null)
                {
                    if (video != null)
                    {
                        articleEntry.video_id = video.id;
                    }
                    articleEntry.active = 1;

                    if (article != null) {
                        article.modified_date = DateTime.Now;
                    }
                    AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        public List<ArticleItem> GetHomePageData()
        {
            List<ArticleItem> articleItems = null;

            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.MergeOption = MergeOption.NoTracking;
                articleItems = AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                    //.Where(w => w.published == 1)
                            .GroupBy(r => r.category_id)
                            .SelectMany(g => g.OrderByDescending(r => r.Article.modified_date).Take(5))
                            .Select(articleEntry => new ArticleItem
                            {
                                ID = articleEntry.id,
                                Author = articleEntry.author,
                                Company = articleEntry.company,
                                Created_Date = articleEntry.Article.created_date,
                                // Content = articleEntry.content_full,
                                Modified_Date = articleEntry.Article.modified_date,
                                Content_summary = articleEntry.content_summary,
                                Title = articleEntry.title,
                                Category = new CategoryItem { Id = articleEntry.ArticleCategory.id, Name = articleEntry.ArticleCategory.name, Name_english = articleEntry.ArticleCategory.name_english },
                                Title_SEO = articleEntry.title_seo,
                                Image_path = articleEntry.Picture.picture_url,
                                Video_url = articleEntry.Video.video_url
                            })
                            .ToList();

            }
            catch (Exception ex)
            {
                // Throw Exception
                return articleItems;
            }
            return articleItems;

        }

        public List<TopNewsItem> GetTopNews(int take_num)
        {

            List<TopNewsItem> topnews = null;
            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.MergeOption = MergeOption.NoTracking;
                topnews = (from news in AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                           select new TopNewsItem { Article_id = news.id, Title = news.title, Title_SEO = news.title_seo, ModifiedDate = news.Article.modified_date, Image_Path = news.Picture.picture_url }).OrderByDescending(e => e.ModifiedDate).Take(take_num).ToList();

            }
            catch (Exception ex)
            {
                // Throw Exception
                return topnews;
            }
            return topnews;
        }

        public List<TagItem> GetTagList()
        {

            List<TagItem> tags = null;
            try
            {
                AppKiMagazineDbContext.DbObjectContext.Tags.MergeOption = MergeOption.NoTracking;
                tags = (from tag in AppKiMagazineDbContext.DbObjectContext.Tags
                        select new TagItem { id = tag.id, name = tag.name }).ToList();

            }
            catch (Exception ex)
            {
                // Throw Exception
                return tags;
            }
            return tags;
        }

        public bool UpdateTagEntry(List<TagItem> tags, int articleID)
        {
            //append articleID for each tag in Tags table
            Tag tag = null;
            try
            {
                AppKiMagazineDbContext.DbObjectContext.Tags.MergeOption = MergeOption.NoTracking;
                foreach (TagItem tagItem in tags)
                {
                    tag = AppKiMagazineDbContext.DbObjectContext.Tags.FirstOrDefault(i => i.name == tagItem.name);
                    if (string.IsNullOrEmpty(tag.articles))
                    {

                        tag.articles = articleID + ",";
                    }
                    else
                    {
                        tag.articles = tag.articles + articleID + ",";
                    }
                }
                AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }

        }

        public List<ArticleItem> GetArticlesByTag(string tag)
        {

            List<ArticleItem> articleItems = null;

            try
            {
                AppKiMagazineDbContext.DbObjectContext.Tags.MergeOption = MergeOption.NoTracking;
                AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.MergeOption = MergeOption.NoTracking;
                string articles = (from tags in AppKiMagazineDbContext.DbObjectContext.Tags where tags.name == tag select tags.articles).FirstOrDefault();
                // var articles_split = articles.Split(',');
                int[] articlearray;

                if (!string.IsNullOrEmpty(articles))
                {
                    articles = articles.TrimEnd(',');
                    articlearray = Helper_Magazine.ToIntArray(articles, ',');
                    articleItems = (from articleEntry in AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                                    where (articlearray.Contains(articleEntry.id))
                                    select new ArticleItem
                                    {
                                        ID = articleEntry.id,
                                        Author = articleEntry.author,
                                        Company = articleEntry.company,
                                        Created_Date = articleEntry.Article.created_date,
                                        Content = articleEntry.content_full,
                                        Content_summary = articleEntry.content_summary,
                                        Title = articleEntry.title,
                                        Category = new CategoryItem { Id = articleEntry.ArticleCategory.id, Name = articleEntry.ArticleCategory.name, Name_english = articleEntry.ArticleCategory.name_english },
                                        Title_SEO = articleEntry.title_seo,                                        
                                        Image_path = articleEntry.Picture.picture_url,
                                        Video_url = articleEntry.Video.video_url, Modified_Date = articleEntry.Article.modified_date                                        
                                    }).OrderByDescending(e=>e.Modified_Date).ToList();
                }




            }

            catch (Exception ex)
            {
                // Throw Exception
                return articleItems;
            }

            return articleItems;
        }

        public bool DeleteArticleEntry(int articleID)
        {

            ArticleEntry articleEntry = null;
            Picture picture = null;
            Video video = null;

            try
            {
                articleEntry = AppKiMagazineDbContext.DbObjectContext.ArticleEntries.FirstOrDefault(i => i.id == articleID);
                if (articleEntry != null)
                {
                    AppKiMagazineDbContext.DbObjectContext.ArticleEntries.DeleteObject(articleEntry);
                }

                picture = AppKiMagazineDbContext.DbObjectContext.Pictures.FirstOrDefault(i => i.article_id == articleID);
                if (picture != null)
                {
                    AppKiMagazineDbContext.DbObjectContext.Pictures.DeleteObject(picture);
                }

                video = AppKiMagazineDbContext.DbObjectContext.Videos.FirstOrDefault(i => i.article_id == articleID);
                if (video != null)
                {
                    AppKiMagazineDbContext.DbObjectContext.Videos.DeleteObject(video);
                }


                AppKiMagazineDbContext.DbObjectContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        private bool DeleteArticleEntry(ArticleEntry articleEntry)
        {

            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticleEntries.DeleteObject(articleEntry);
                AppKiMagazineDbContext.DbObjectContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        private bool DeleteArticlePublished(ArticlePublished article)
        {

            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.DeleteObject(article);
                AppKiMagazineDbContext.DbObjectContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        public bool DeleteArticleArchived(int articleID)
        {

            ArticleArchived articleEntry = null;
            Picture picture = null;
            Video video = null;

            try
            {
                articleEntry = AppKiMagazineDbContext.DbObjectContext.ArticleArchiveds.FirstOrDefault(i => i.id == articleID);
                if (articleEntry != null)
                {
                    AppKiMagazineDbContext.DbObjectContext.ArticleArchiveds.DeleteObject(articleEntry);
                }

                picture = AppKiMagazineDbContext.DbObjectContext.Pictures.FirstOrDefault(i => i.article_id == articleID);
                if (picture != null)
                {
                    AppKiMagazineDbContext.DbObjectContext.Pictures.DeleteObject(picture);
                }

                video = AppKiMagazineDbContext.DbObjectContext.Videos.FirstOrDefault(i => i.article_id == articleID);
                if (video != null)
                {
                    AppKiMagazineDbContext.DbObjectContext.Videos.DeleteObject(video);
                }


                AppKiMagazineDbContext.DbObjectContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        public bool ApproveArticle(int articleID, Guid userid, string userRole)
        {

            ArticleEntry articleEntry = null;
            Article article = null;

            try
            {
                articleEntry = AppKiMagazineDbContext.DbObjectContext.ArticleEntries.FirstOrDefault(i => i.id == articleID);
                article = AppKiMagazineDbContext.DbObjectContext.Articles.FirstOrDefault(i => i.id == articleID);
                if (articleEntry != null)
                {
                    if (userRole == "admin" || userRole == "ceditor" || userRole == "sadmin" || (userid == articleEntry.user_id))
                    {

                         ArticlePublished articlepublished = new ArticlePublished
                            {
                                id = articleEntry.id,
                                active = articleEntry.active,
                                author = articleEntry.author,
                                category_id = articleEntry.category_id,
                                company = articleEntry.company,
                                content_full = articleEntry.content_full,
                                content_summary = articleEntry.content_summary,
                                isbreakingnews = articleEntry.isbreakingnews,
                                video_id = articleEntry.video_id,
                                user_id = articleEntry.user_id,
                                title_seo = articleEntry.title_seo,
                                title_english = articleEntry.title_english,
                                tags = articleEntry.tags,
                                title = articleEntry.title,
                                picture_id = articleEntry.picture_id,
                                approver_id = userid

                            };
                            AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.AddObject(articlepublished);
                            
                            if (article != null) {
                                article.modified_date = DateTime.Now;
                            }

                            AppKiMagazineDbContext.DbObjectContext.ArticleEntries.DeleteObject(articleEntry);
                            AppKiMagazineDbContext.DbObjectContext.SaveChanges();

                           
                        
                    }
                   

                }

                return true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        public bool RejectArticle(int articleID, Guid userid, string userRole)
        {

            ArticleEntry articleEntry = null;
            try
            {
                articleEntry = AppKiMagazineDbContext.DbObjectContext.ArticleEntries.FirstOrDefault(i => i.id == articleID);
                if (articleEntry != null)
                {
                    if (userRole == "admin" || userRole == "ceditor" || userRole == "sadmin" || (userid == articleEntry.user_id))
                    {
                        articleEntry.approver_id = userid;
                        articleEntry.isRejected = 1;
                        AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                    }
                   
                }

                return true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        public bool ArchiveArticle(int articleID, Guid userid, string userRole)
        {

            ArticlePublished articlePublished = null;
            try
            {
                articlePublished = AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.FirstOrDefault(i => i.id == articleID);
                if (articlePublished != null)
                {
                    if (userRole == "admin" || userRole == "ceditor" || userRole == "sadmin" || (userid == articlePublished.user_id))
                    {

                        
                            ArticleArchived articlearchived = new ArticleArchived
                            {
                                id = articlePublished.id,
                                active = articlePublished.active,
                                author = articlePublished.author,
                                category_id = articlePublished.category_id,
                                company = articlePublished.company,
                                content_full = articlePublished.content_full,
                                content_summary = articlePublished.content_summary,
                                isbreakingnews = articlePublished.isbreakingnews,
                                video_id = articlePublished.video_id,
                                user_id = articlePublished.user_id,
                                title_seo = articlePublished.title_seo,
                                title_english = articlePublished.title_english,
                                tags = articlePublished.tags,
                                title = articlePublished.title,
                                isRejected = articlePublished.isRejected,
                                remark = articlePublished.remark,
                                picture_id = articlePublished.picture_id,
                                approver_id = userid

                            };
                            AppKiMagazineDbContext.DbObjectContext.ArticleArchiveds.AddObject(articlearchived);
                            //AppKiMagazineDbContext.DbObjectContext.SaveChanges(false);


                            AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.DeleteObject(articlePublished);
                            AppKiMagazineDbContext.DbObjectContext.SaveChanges();

                           
                        

                    }


                }

                return true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }

        public bool UpdateProfiles(List<UserProfileItem> profileItems)
        {
            PortalUserProfile portalUserProfile = null;
            try
            {
                foreach (UserProfileItem item in profileItems)
                {
                    portalUserProfile = AppKiMagazineDbContext.DbObjectContext.PortalUserProfiles.FirstOrDefault(i => i.User_Id == item.ID);

                    portalUserProfile.Role = item.Role;

                }
                AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }


        public bool UpdateBreakingNews(List<NewsItem> breakingNews)
        {
            BreakingNew newsItem = null;
            try
            {
                foreach (NewsItem item in breakingNews)
                {
                    newsItem = AppKiMagazineDbContext.DbObjectContext.BreakingNews.FirstOrDefault(i => i.id == item.Id);

                    if (item.Article_id == null)
                    {
                        newsItem.article_id = null;
                        newsItem.custom_text = item.CustomText;
                    }
                    else
                    {
                        newsItem.article_id = item.Article_id;
                        newsItem.custom_text = null;
                    }

                }
                AppKiMagazineDbContext.DbObjectContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                return false;
            }
        }


        public List<ArticleItem> GetArticlesByCategory(string categoryType, int take_num)
        {

            List<ArticleItem> articleItems = null;

            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.MergeOption = MergeOption.NoTracking;
                articleItems = (from articleEntry in AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                                where (articleEntry.ArticleCategory.name_english == categoryType)
                                select new ArticleItem
                                {
                                    ID = articleEntry.id,
                                    Author = articleEntry.author,
                                    Company = articleEntry.company,
                                    Modified_Date = articleEntry.Article.modified_date,
                                    Title = articleEntry.title,
                                    Category = new CategoryItem { Id = articleEntry.ArticleCategory.id, Name = articleEntry.ArticleCategory.name, Name_english = articleEntry.ArticleCategory.name_english },
                                    Title_SEO = articleEntry.title_seo,
                                    Image_path = articleEntry.Picture.picture_url,
                                    Video_url = articleEntry.Video.video_url
                                }).OrderByDescending(a => a.Modified_Date).Take(take_num).ToList();

            }

            catch (Exception ex)
            {
                // Throw Exception
                return articleItems;
            }

            return articleItems;
        }

        public List<RelatedNewsItem> GetRelatedNews(string categoryType, int take_num)
        {

            List<RelatedNewsItem> relatedNews = null;
            try
            {
                AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds.MergeOption = MergeOption.NoTracking;
                relatedNews = (from news in AppKiMagazineDbContext.DbObjectContext.ArticlePublisheds
                               where (news.ArticleCategory.name_english != categoryType)
                               select new RelatedNewsItem { Article_id = news.id, Title = news.title, Title_SEO = news.title_seo, ModifiedDate = news.Article.modified_date, Image_Path = news.Picture.picture_url })
                               .OrderByDescending(e => e.ModifiedDate).Take(take_num).ToList();

            }
            catch (Exception ex)
            {
                // Throw Exception
                return relatedNews;
            }
            return relatedNews;
        }
    }
}