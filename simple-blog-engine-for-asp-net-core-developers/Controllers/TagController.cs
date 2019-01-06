using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleBlogEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBlogEngine.Controllers
{
    public class TagController : BaseController
    {
        private BlogPostsSettings _blogPostsConfig;
        public TagController(
            IHostingEnvironment hostingEnvironment,
            IOptionsMonitor<BlogPostsSettings> blogPostsConfig) : base(hostingEnvironment)
        {
            _blogPostsConfig = blogPostsConfig.CurrentValue;
        }

        public IActionResult Index(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return Redirect("~/tags");
            }

            var model = new BlogPostsViewModel(_blogPostsConfig)
            {
                PageOfBlogPosts = _blogPostsConfig.GetBlogPostsWithTag(tag),
                Tag = tag
            };
            return View(model);
        }
        public IActionResult All()
        {
            List<string> allTags = new List<string>();

            foreach (var item in _blogPostsConfig.Blogs.Where(x => x.Published))
            {
                allTags = allTags.Union(item.Tags).ToList();
            }

            return View(allTags);
        }
    }
}
