using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;
using SimpleBlogEngine.Models;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace SimpleBlogEngine.Controllers
{
    public class HomeController : BaseController
    {
        private BlogPostsSettings _blogPostsConfig;

        public HomeController(
            IHostingEnvironment hostingEnvironment,
            IOptionsMonitor<BlogPostsSettings> blogPostsConfig) : base(hostingEnvironment)
        {
            _blogPostsConfig = blogPostsConfig.CurrentValue;
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
            return new FeedResult(_blogPostsConfig, "Rss");
        }
        public ContentResult Atom()
        {
            return new FeedResult(_blogPostsConfig, "Atom");
        }
    }

    public class FeedResult : ContentResult
    {
        private BlogPostsSettings _blogPostsConfig;
        public FeedResult(BlogPostsSettings blogPostsConfig, string feedType)
        {
            _blogPostsConfig = blogPostsConfig;

            var sw = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { Async = true, Indent = true }))
            {
                XmlFeedWriter writer = feedType == "Atom" ? (XmlFeedWriter)new AtomFeedWriter(xmlWriter) : new RssFeedWriter(xmlWriter);
                // TODO: Make a cleaner solution, connect here to siteSettings
                if (writer as AtomFeedWriter != null)
                {
                    ((AtomFeedWriter)writer).WriteTitle("Asozyurt Blog");
                    ((AtomFeedWriter)writer).WriteSubtitle("Asozyurt Son Blog Yazıları");
                    ((AtomFeedWriter)writer).WriteRights("(c) 2019 Asozyurt");
                    ((AtomFeedWriter)writer).WriteUpdated(_blogPostsConfig.Blogs.Where(x => x.Published).Max(x => x.CreateDate.Date).Date);
                    ((AtomFeedWriter)writer).WriteGenerator("Asozyurt", "http://asozyurt.com", "1");
                }
                else
                {
                    ((RssFeedWriter)writer).WriteTitle("Asozyurt Blog");
                    ((RssFeedWriter)writer).WriteDescription("Asozyurt Son Blog Yazıları");
                    ((RssFeedWriter)writer).WriteCopyright("(c) 2019 Asozyurt");
                    ((RssFeedWriter)writer).WriteGenerator("Asozyurt");
                    ((RssFeedWriter)writer).WritePubDate(_blogPostsConfig.Blogs.Where(x => x.Published).Max(x => x.CreateDate.Date).Date);
                }

                foreach (var post in _blogPostsConfig.Blogs.Where(x => x.Published))
                {
                    // Create item
                    var item = new SyndicationItem()
                    {
                        Title = post.Title,
                        Description = post.Description,
                        Id = post.Slug,
                        Published = post.CreateDate.Date,
                        LastUpdated = post.CreateDate.Date
                    };

                    post.Categories.ForEach(x => item.AddCategory(new SyndicationCategory(x)));
                    item.AddLink(new SyndicationLink(new System.Uri("http://blog.asozyurt.com/" + post.CreateDate.Year.ToString() + "/" + post.CreateDate.Month.ToString("00") + "/" + post.Slug)));
                    item.AddContributor(new SyndicationPerson(post.Author, "asozyurt@gmail.com"));
                    writer.Write(item);
                }
                xmlWriter.Flush();
            }
            Content = sw.ToString();
            ContentType = "application/xml";
        }
    }
}
