using System.Collections.Generic;

namespace SimpleBlogEngine.Models
{
    public class BlogPostsViewModel : BlogPostsBaseViewModel
    {
        public BlogPostsViewModel(BlogPostsSettings blogPostConfig) : base(blogPostConfig)
        {
        }
        public List<BlogPost> PageOfBlogPosts { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public int CurrentPageNumber { get; set; }
    }
}
