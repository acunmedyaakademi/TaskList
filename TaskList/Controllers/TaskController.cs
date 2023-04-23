﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult TaskDetail(Guid id)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            return View(_taskService.GetTaskById(id));
        }
        public IActionResult DeleteTask(Guid TaskId)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            _taskService.DeleteTask(TaskId);
            return View();
        }
        public IActionResult AssignedTasks()
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            return View(_taskService.GetGivenTasks(new Guid(HttpContext.Session.GetString("LoginId"))));
        }

        [Route("task/createtask/{userId}")]
        public IActionResult CreateTask(string userId)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            HttpContext.Session.SetString("AssingerId", userId);
            //return Content(userId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTask(Task task)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            task.Id = Guid.NewGuid();
            task.AssingerId = new Guid(HttpContext.Session.GetString("AssingerId"));
            task.AssignedById = new Guid(HttpContext.Session.GetString("LoginId"));
            task.IsDone = false;
            task.CreatedOn = DateTime.Now;
            task.UpdatedOn = DateTime.Now;
            task.IsActive = true;
            _taskService.AddTask(task);
            return View();
        }

        public IActionResult UpdateTask()
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

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
