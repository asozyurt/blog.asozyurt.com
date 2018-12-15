using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogEngine.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace SimpleBlogEngine.Controllers
{
	public class BlogController:BaseController
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
			if(blogPost == null)
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
            var blogPost = _blogPostsConfig.Blogs.Where(x => x.Published).OrderByDescending(x => x.CreateDate);
            return blogPost.ToList();
        }

    }
}
