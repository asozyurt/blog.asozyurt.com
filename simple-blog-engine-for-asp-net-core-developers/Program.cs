using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SimpleBlogEngine
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args)
		{
			var myConfig = new ConfigurationBuilder()
			  .SetBasePath(Directory.GetCurrentDirectory())
			  .AddJsonFile("Settings" + Path.DirectorySeparatorChar + "System" + Path.DirectorySeparatorChar + "hosting.json",optional: false)
			  .Build();

            return WebHost.CreateDefaultBuilder(args)
			  .ConfigureLogging((hostingContext, logging) =>
			  {
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddConsole();
					logging.AddDebug();
			  })
			  .UseConfiguration(myConfig)
			  .UseStartup<Startup>()
			  .Build();
		}
	}
}

