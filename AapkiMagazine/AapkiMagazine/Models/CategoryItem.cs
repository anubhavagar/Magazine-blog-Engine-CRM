using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AapkiMagazine.Models
{
    public class CategoryItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Name_english { get; set; }
    }

    public class TagItem
    {
        public int id { get; set; }

        public string name { get; set; }
    }

    public class UserProfileItem
    {
        public Guid ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Role { get; set; }
        public string EmailId { get; set; }
        public bool Valid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public IList<SelectListItem> Roles { get; set; }

    }

    public class PageNotFound {
       public string Error { get; set; }
       public string Message { get; set; }
    }
}