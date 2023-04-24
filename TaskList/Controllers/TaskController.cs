using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskList.Business.Abstract;
using TaskList.Models.ViewModels;
using TaskList.Models.ViewModels.TaskViewModels;
using Task = TaskList.Models.Task;

namespace TaskList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult TaskDetail(Guid id)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            return View(_taskService.GetTaskById(id));
        }


        public IActionResult DeleteTask(string TaskId)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            _taskService.DeleteTask(new Guid(TaskId));
            ResponseModel response = _taskService.DeleteTask(new Guid(TaskId));
            if (response.Success)
                return View();

            return Content("hata");
        }
        public IActionResult AssignedTasks()
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            return View(_taskService.GetGivenTasks(new Guid(HttpContext.Session.GetString("LoginId"))));
        }

        public IActionResult UpdateTask(string id)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");
            UpdateTaskModel model = new UpdateTaskModel();
            model.task = _taskService.GetTask(new Guid(id));
            model.Users = _userService.GetUsers();
            return View(model);
        }
        public IActionResult CreateTask(string id)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            HttpContext.Session.SetString("alan", id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTask(CreateTaskModel taskModel)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            Task task = new Task();
            task.Id = Guid.NewGuid();
            task.AssingerId = new Guid(HttpContext.Session.GetString("LoginId"));
            task.AssignedById = new Guid(HttpContext.Session.GetString("alan"));
            task.IsDone = false;
            task.CreatedOn = DateTime.Now;
            task.UpdatedOn = DateTime.Now;
            task.IsActive = true;
            task.TaskDescription = taskModel.TaskDescription;
            task.TaskName = taskModel.TaskName;
            ResponseModel response = _taskService.AddTask(task);
            if (response.Success)
            {
                RedirectToAction("users", "user");
            }
            ViewBag.Create = response.Message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTask(Task task)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            _taskService.UpdateTask(task);
            return View();
        }


    }
}
