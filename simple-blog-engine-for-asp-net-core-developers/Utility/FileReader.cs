using SimpleBlogEngine.Models;
using System.IO;

namespace blog.asozyurt.com.Utility
{
    public class FileReader
    {
        public static string GetBlogPostContent(BlogPost blogPost)
        {
            // TODO: Add cache support for reducing file read operations 
            try
            {
                return File.ReadAllText("Views/_blogSource/"+blogPost.View);
            }
            catch
            {
                return "#Olmadı";
            }
        }
    }
}
