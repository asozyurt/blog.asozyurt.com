using Markdig;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SimpleBlogEngine.Models;
using System;
using System.IO;

namespace blog.asozyurt.com.Utility
{
    public class BlogPostContentReader
    {
        public static string GetContent(BlogPost blogPost, IMemoryCache cache, ILogger logger)
        {
            string cacheEntry = string.Empty;
            try
            {
                if (!cache.TryGetValue(blogPost.Slug, out cacheEntry))
                {
                    string path = "Views" + Path.DirectorySeparatorChar + "_blogSource" + Path.DirectorySeparatorChar + blogPost.View;
                    if (File.Exists(path))
                    {
                        cacheEntry = Markdown.ToHtml(File.ReadAllText(path));

                        // Set cache options.
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromHours(6));

                        // Save data in cache.
                        cache.Set(blogPost.Slug, cacheEntry, cacheEntryOptions);
                    }
                    else
                    {
                        logger.LogWarning("File Not Found at BlogPostContentReader.GetContent : " + path);
                        return "Blog Post cannot be found - " + blogPost.View;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error at BlogPostContentReader.GetContent with exception: " + ex);
            }

            return cacheEntry;
        }
    }
}
