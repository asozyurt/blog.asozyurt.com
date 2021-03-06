﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleBlogEngine.Models;

namespace SimpleBlogEngine.Controllers
{
    public class CategoryController : BaseController
    {
        private BlogPostsSettings _blogPostsConfig;
        public CategoryController(
            IHostingEnvironment hostingEnvironment,
            IOptionsMonitor<BlogPostsSettings> blogPostsConfig) : base(hostingEnvironment)
        {
            _blogPostsConfig = blogPostsConfig.CurrentValue;
        }

        public IActionResult Index(string category)
        {
            var model = new BlogPostsViewModel(_blogPostsConfig)
            {
                PageOfBlogPosts = _blogPostsConfig.GetBlogPostsWithCategory(category),
                Category = category
            };
            return View(model);
        }
    }
}
