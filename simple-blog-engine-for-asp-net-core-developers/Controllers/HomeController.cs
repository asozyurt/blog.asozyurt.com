using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;
using SimpleBlogEngine.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SimpleBlogEngine.Controllers
{
    public class HomeController : BaseController
    {
        private BlogPostsSettings _blogPostsConfig;
        private SiteSettings _siteSettings;

        public HomeController(
            IHostingEnvironment hostingEnvironment,
            IOptionsMonitor<BlogPostsSettings> blogPostsConfig,
            IOptionsMonitor<SiteSettings> siteSettings) : base(hostingEnvironment)
        {
            _blogPostsConfig = blogPostsConfig.CurrentValue;
            _siteSettings = siteSettings.CurrentValue;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            var model = new BlogPostsViewModel(_blogPostsConfig)
            {
                PageOfBlogPosts = _blogPostsConfig.GetPage(pageNumber),
                CurrentPageNumber = pageNumber
            };
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ContentResult Rss()
        {
            return FeedResult.Rss(_siteSettings, _blogPostsConfig);
        }
        public ContentResult Atom()
        {
            return FeedResult.Atom(_siteSettings, _blogPostsConfig);
        }
    }

    public class FeedResult : ContentResult
    {
        private BlogPostsSettings _blogPostsConfig;
        private SiteSettings _siteSettings;

        private IList<SyndicationItem> GetFeed()
        {
            IList<SyndicationItem> result = new List<SyndicationItem>();

            foreach (var post in _blogPostsConfig.Blogs.Where(x => x.Published))
            {
                var item = new SyndicationItem()
                {
                    Title = post.Title,
                    Description = post.Description,
                    Id = post.Slug,
                    Published = post.CreateDate.Date,
                    LastUpdated = post.CreateDate.Date
                };

                post.Categories.ForEach(x => item.AddCategory(new SyndicationCategory(x)));
                item.AddLink(new SyndicationLink(new System.Uri(_siteSettings.SiteURL + "/" + post.UrlTail)));
                item.AddContributor(new SyndicationPerson(post.Author, _siteSettings.Email));
                result.Add(item);
            }
            return result;
        }

        private XmlFeedWriter InitializeAtomFeedWriter(XmlWriter xmlWriter)
        {
            var result = new AtomFeedWriter(xmlWriter);
            result.WriteTitle(_siteSettings.SiteName);
            result.WriteSubtitle(_siteSettings.Description);
            result.WriteRights(_siteSettings.Copyright);
            result.WriteUpdated(_blogPostsConfig.Blogs.Where(x => x.Published).First().CreateDate.Date);
            result.WriteGenerator(_siteSettings.Nickname, _siteSettings.PersonalSiteURL, _siteSettings.Version);
            return result;
        }
        private XmlFeedWriter InitializeRssFeedWriter(XmlWriter xmlWriter)
        {
            var result = new RssFeedWriter(xmlWriter);
            result.WriteTitle(_siteSettings.SiteName);
            result.WriteDescription(_siteSettings.Description);
            result.WriteCopyright(_siteSettings.Copyright);
            result.WriteGenerator(_siteSettings.Nickname);
            result.WritePubDate(_blogPostsConfig.Blogs.Where(x => x.Published).First().CreateDate.Date);
            return result;
        }

        private FeedResult(SiteSettings siteSettings, BlogPostsSettings blogPostsConfig, string feedType = "Rss")
        {
            _siteSettings = siteSettings;
            _blogPostsConfig = blogPostsConfig;

            var sw = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { Async = true, Indent = true, Encoding = Encoding.UTF8 }))
            {
                XmlFeedWriter writer = "Atom".Equals(feedType) ? InitializeAtomFeedWriter(xmlWriter) : InitializeRssFeedWriter(xmlWriter);
                foreach (var post in GetFeed())
                {
                    writer.Write(post);
                }
                xmlWriter.Flush();
            }
            Content = sw.ToString().Replace("utf-16", "utf-8");
            ContentType = "application/rss+xml; charset=utf-8";
        }

        /// <summary>
        /// Returns Rss Feed Result
        /// </summary>
        /// <param name="siteSettings"></param>
        /// <param name="blogPostsConfig"></param>
        /// <returns></returns>
        public static FeedResult Rss(SiteSettings siteSettings, BlogPostsSettings blogPostsConfig)
        {
            return new FeedResult(siteSettings, blogPostsConfig);
        }
        /// <summary>
        /// Returns Atom Feed Result
        /// </summary>
        /// <param name="siteSettings"></param>
        /// <param name="blogPostsConfig"></param>
        /// <returns></returns>
        public static FeedResult Atom(SiteSettings siteSettings, BlogPostsSettings blogPostsConfig)
        {
            return new FeedResult(siteSettings, blogPostsConfig, "Atom");
        }
    }
}
