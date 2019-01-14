using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleBlogEngine.Models
{
    public class BlogPost
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
        public BlogPostDate CreateDate { get; set; }
        public BlogPostDate LastUpdate { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Categories { get; set; }
        public string Author { get; set; }
        public string Slug { get; set; }
        public string View { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }

        /// <summary>
        /// Gets the prepared url which is tailing after site base address
        /// </summary>
        public string UrlTail
        {
            get
            {
                return CreateDate.Year.ToString() + "/" + CreateDate.Month.ToString("00") + "/" + Slug;
            }
        }
    }

    public class BlogPostDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public string Display { get; set; }
        public DateTime Date { get { return new DateTime(Year, Month, Day, Hour, Minute, Second); } }
    }
}
