using Microsoft.AspNetCore.Mvc;
using TaskList.Business.Abstract;
using TaskList.Models.ViewModels;
using Task = TaskList.Models.Task;

namespace TaskList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult TaskDetail(Guid id)
        {

            return View(_taskService.GetTaskById(id));
        }
        public IActionResult CreateTask()
        {
            return View();
        }
        public IActionResult DeleteTask(string TaskId)
        {
            ResponseModel response = _taskService.DeleteTask(new Guid(TaskId));
            if (response.Success)
            return View();

            return Content("hata");
        }
        public IActionResult AssignedTasks()
        {
            return View(_taskService.GetGivenTasks(new Guid(HttpContext.Session.GetString("LoginId"))));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTask(Task task)
        {
            _taskService.AddTask(task);
            return View();
        }

        public IActionResult UpdateTask(string id)
        {
            return View(_taskService.GetTask(new Guid(id)));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTask(Task task)
        {
            _taskService.UpdateTask(task);
            return View();
        }

        
    }
}
