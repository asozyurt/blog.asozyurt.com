using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleBlogEngine.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SimpleBlogEngine.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class BlogController : BaseController
    {
        private BlogPostsSettings _blogPostsConfig;
        public BlogController(
            IHostingEnvironment hostingEnvironment,
            IOptionsMonitor<BlogPostsSettings> blogPostsConfig) : base(hostingEnvironment)
        {
            _blogPostsConfig = blogPostsConfig.CurrentValue;
        }

        public IActionResult ViewBlogPost(int year, int month, string slug)
        {
            var blogPost = _blogPostsConfig.Blogs.SingleOrDefault(b => b.Slug == slug);
            if (blogPost == null)
            {
                return this.NotFound();
            }
            return View(blogPost);
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<BlogPost> GetRecentBlogPosts()
        {
            try
            {
                var blogPost = _blogPostsConfig.Blogs.Where(x => x.Published).OrderByDescending(x => x.CreateDate.Date).Take(_blogPostsConfig.NumberOfRecentBlogPostsToServeInApi);
                return blogPost.ToList();
            }
            catch
            {
                // TODO: Log error
                return null;
            }
        }

    }
}
