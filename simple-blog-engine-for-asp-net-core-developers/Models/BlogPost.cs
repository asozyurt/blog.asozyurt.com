using System;
using System.Collections.Generic;

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
    }

    public class BlogPostDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Display { get; set; }
        public DateTime Date { get { return new DateTime(Year, Month, Day); } }
    }
}
