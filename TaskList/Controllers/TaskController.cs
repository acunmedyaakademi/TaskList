using Microsoft.AspNetCore.Mvc;

namespace TaskList.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TaskDetail()
        {
            return View();
        }

        public IActionResult CreateTask()
        {
            return View();
        }
    }
}
