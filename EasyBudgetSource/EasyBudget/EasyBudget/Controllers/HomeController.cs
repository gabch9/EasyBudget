using System.Web.Mvc;

namespace EasyBudget.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}