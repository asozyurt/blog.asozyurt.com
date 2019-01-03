using blog.asozyurt.com.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleBlogEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBlogEngine.Controllers
{
    public class SearchController : BaseController
    {
        private BlogPostsSettings _blogPostsConfig;
        private IMemoryCache _cache;
        private ILogger _logger;

        public SearchController(
            IHostingEnvironment hostingEnvironment,
            IOptionsMonitor<BlogPostsSettings> blogPostsConfig, IMemoryCache cache, ILogger<SearchController> logger) : base(hostingEnvironment)
        {
            _blogPostsConfig = blogPostsConfig.CurrentValue;
            _cache = cache;
            _logger = logger;
        }

        public IActionResult Index(string searchText)
        {
            List<BlogPost> result = new List<BlogPost>();
            if (!string.IsNullOrEmpty(searchText) && searchText.Length > 2)
            {

                foreach (var post in _blogPostsConfig.Blogs)
                {
                    // Add if a category of the post matches the search text
                    if (post.Categories.Any(c => c.Equals(searchText, StringComparison.OrdinalIgnoreCase)))
                    {
                        result.Add(post);
                        continue;
                    }

                    // Add if a tag of the post matches the search text
                    if (post.Tags.Any(c => c.Equals(searchText, StringComparison.OrdinalIgnoreCase)))
                    {
                        result.Add(post);
                        continue;
                    }

                    if (string.IsNullOrEmpty(post.Content))
                    {
                        post.Content = BlogPostContentReader.GetContent(post, _cache, _logger);
                    }

                    if (post.Content.Contains(searchText,StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(post);
                    }
                }
            }
            var model = new BlogPostsViewModel(_blogPostsConfig)
            {
                SearchText = searchText,
                PageOfBlogPosts = result
            };
            return View(model);
        }
    }
}
