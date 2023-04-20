using Microsoft.AspNetCore.Mvc;

namespace TaskList.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
