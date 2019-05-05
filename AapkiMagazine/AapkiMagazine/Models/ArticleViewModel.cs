using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AapkiMagazine.Models
{
    public class ArticleViewModel
    {
        //public ArticleViewModel()
        //{
        //    this.ID = Guid.NewGuid();
        //    this.Created_Date = DateTime.Now;            
        //}

        public List<CategoryItem> Categories { get; set; }
       // public List<TagItem> Tags { get; set; }
        public string Tags { get; set; }
        public ArticleItem ArticleItem { get; set; }
        public List<string> Errors { get; set; }
        //public UserProfileItem Userprofile { get; set; }

    }

    public class ArticleItem
    {
        public string Title { get; set; }
        public string Title_English { get; set; }
        public string Title_SEO { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Content { get; set; }
        public CategoryItem Category { get; set; }
        public string Author { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public string Company { get; set; }
        [DataType(DataType.MultilineText)]
        public string Tag_Article { get; set; }
        public int ID { get; set; }
        public string Image_path { get; set; }
        public string Video_url { get; set; }       
        public bool validyturl { get; set; }
        public Guid Userid { get; set; }
        public string EmailID { get; set; }
        [DataType(DataType.MultilineText)]
        public byte? active { get; set; }
        [AllowHtml]
        public string Content_summary { get; set; }
        public List<TagItem> Tags { get; set; }
    }
}