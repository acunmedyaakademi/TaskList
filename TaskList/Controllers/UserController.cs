using Microsoft.AspNetCore.Mvc;
using TaskList.Business.Abstract;
using TaskList.Models.ViewModels.UserViewModels;

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
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            return View();
        }

        [Route("user/userstasks/{id}")]
        public IActionResult UsersTasks(string id)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            //return Content(id);
            return View();
        }
        public IActionResult Users()
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            List<GetUserModel> users =  _userService.GetUsers();

            return View(users);
        }
    }
}
