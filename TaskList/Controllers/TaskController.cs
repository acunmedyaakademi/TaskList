using Microsoft.AspNetCore.Mvc;
using TaskList.Business.Abstract;
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

        public IActionResult TaskDetail()
        {
            return View();
        }
        public IActionResult CreateTask()
        {
            return View();
        }
        public IActionResult DeleteTask(Guid TaskId)
        {
            _taskService.DeleteTask(TaskId);
            return View();
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

        public IActionResult UpdateTask()
        {
            return View();
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
