using Microsoft.AspNetCore.Mvc;

namespace bookverse.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
