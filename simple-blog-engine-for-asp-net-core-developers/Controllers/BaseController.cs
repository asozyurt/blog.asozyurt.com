using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleBlogEngine.Controllers
{
	public class BaseController:Controller
	{
		private readonly IHostingEnvironment _hostingEnvironment;

		public BaseController(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
			
		}
	}
}