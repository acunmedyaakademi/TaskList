using Microsoft.AspNetCore.Mvc;

namespace TaskList.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
