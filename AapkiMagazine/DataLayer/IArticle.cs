using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataLayer
{
    public abstract class ArticleBase
    {
        public ArticleBase()
        {
            this.id = Guid.NewGuid();
            this.created_date = DateTime.Now;
        }

        public Guid id { get; set; }

        public DateTime created_date { get; set; }

        public DateTime? modified_date { get; set; }
    }

    public class ArticleEntry :ArticleBase {

        private string title;

        public string Title {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.Title_URL = Regex.Replace(
                    value.ToLowerInvariant().Replace(" - ", "-").Replace(" ", "-"),
                    "[^\\w^-]",
                    string.Empty);
            }
        
        }

        public string Type { get; set; }

        public string Title_URL { get; set; }

        public string Content_Full { get; set; }

        public string Author { get; set; }

        public bool Active { get; set; }

        public string Picture_URL { get; set; }

        public string Video_URL { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }

    public class Tag : ArticleBase
    {
         public string Name { get; set; }

         public virtual ICollection<ArticleEntry> Article_Entries { get; set; }
    }

   
}

