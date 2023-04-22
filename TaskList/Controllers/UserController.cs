using Microsoft.AspNetCore.Mvc;

namespace TaskList.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UsersTasks()
        {
            return View();
        }
    }
}
