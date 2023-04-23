using Microsoft.AspNetCore.Mvc;
using TaskList.Business.Abstract;

namespace TaskList.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;

        public UserController(IUserService userService, ITaskService taskService)
        {
            _userService = userService;
            _taskService = taskService;
        }

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
