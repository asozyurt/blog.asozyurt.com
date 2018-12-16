using blog.asozyurt.com.Models;
using blog.asozyurt.com.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleBlogEngine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SimpleBlogEngine.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class BlogController : BaseController
    {
        private IMemoryCache _cache;
        private ILogger _logger;
        private BlogPostsSettings _blogPostsConfig;
        public BlogController(
            IHostingEnvironment hostingEnvironment, IOptionsMonitor<BlogPostsSettings> blogPostsConfig, IMemoryCache cache, ILogger<BlogController> logger)
            : base(hostingEnvironment)
        {
            _blogPostsConfig = blogPostsConfig.CurrentValue;
            _cache = cache;
            _logger = logger;
        }

        public IActionResult ViewBlogPost(int year, int month, string slug)
        {
            var blogPost = _blogPostsConfig.Blogs.SingleOrDefault(b => b.Slug == slug);
            if (blogPost == null)
            {
                return this.NotFound();
            }
            blogPost.Content = BlogPostContentReader.GetContent(blogPost, _cache, _logger);
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
            List<BlogPost> cacheEntry = null;
            try
            {
                if (!_cache.TryGetValue(CacheKeys.RecentBlogPosts, out cacheEntry))
                {
                    var blogPosts = _blogPostsConfig.Blogs.Where(x => x.Published).OrderByDescending(x => x.CreateDate.Date).Take(_blogPostsConfig.NumberOfRecentBlogPostsToServeInApi).ToList();
                    blogPosts.ForEach(x => x.Content = string.Empty);
                    cacheEntry = blogPosts;
                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                    // Save data in cache.
                    _cache.Set(CacheKeys.RecentBlogPosts, cacheEntry, cacheEntryOptions);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error at GetRecentBlogPosts with exception: " + ex);
            }

            return cacheEntry;
        }

    }
}
