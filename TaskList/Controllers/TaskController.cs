using AspNetCore;
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
        readonly ICommentService _commentService;

        public TaskController(ITaskService taskService, IUserService userService, ICommentService commentService)
        {
            _taskService = taskService;
            _userService = userService;
            _commentService = commentService;
        }

        public IActionResult TaskDetail(string id)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            TaskDetailContainer model = new TaskDetailContainer();

            model.Task = _taskService.GetTask(new Guid(id));
            model.Comments = _commentService.GetComments(new Guid(id));
            if (HttpContext.Session.GetString("CommentMessage") is not null)
                ViewBag.CommentMessage = HttpContext.Session.GetString("CommentMessage");
            return View(model);
        }

        public IActionResult DeleteTask(string id)
        {
            try
            {
                if (HttpContext.Session.GetString("LoginName") == null)
                    return RedirectToAction("login", "account");
                ResponseModel response = _taskService.DeleteTask(new Guid(id));
                if (response.Success)
                    return RedirectToAction("AssignedTasks", "Task");

                ViewBag.DeleteTask = "Islemde hata olusdu";
                return RedirectToAction("AssignedTasks", "Task");
            }
            catch
            {
                ViewBag.DeleteTask = "Islemde hata olusdu";
                return RedirectToAction("AssignedTasks", "Task");
            }


            //return Content("hata");
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

            HttpContext.Session.SetString("UpdateTask", id);
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
            if (!ModelState.IsValid)
            {
                ViewBag.Create = "Model valid degil";
                return View();
            }
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

            task.Id = new Guid(HttpContext.Session.GetString("UpdateTask"));
            task.AssingerId = new Guid(HttpContext.Session.GetString("LoginId"));
            task.IsDone = false;
            ResponseModel response = _taskService.UpdateTask(task);
            if (response.Success)
            {
                return RedirectToAction("AssignedTasks", "Task");
            }
            ViewBag.Update = response.Message;
            return RedirectToAction("AssignedTasks", "Task");

        }
        public IActionResult DoneTask(string id)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            ResponseModel response = _taskService.DoneTask(new Guid(id));

            if (response.Success)
            {
                return RedirectToAction("index", "home");
            }
            return RedirectToAction("hata", "hata");

        }

        public IActionResult UndoneTask(string id)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            ResponseModel response = _taskService.UndoneTask(new Guid(id));

            if (response.Success)
            {
                return RedirectToAction("index", "home");
            }
            return RedirectToAction("hata", "hata");

        }


    }
}
