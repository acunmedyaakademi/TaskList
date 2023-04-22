using Microsoft.AspNetCore.Mvc;

namespace TaskList.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
