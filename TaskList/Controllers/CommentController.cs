using Microsoft.AspNetCore.Mvc;
using TaskList.Business.Abstract;
using TaskList.Models;
using TaskList.Models.ViewModels;

namespace TaskList.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ITaskService _taskService;

        public CommentController(ICommentService commentService, ITaskService taskService)
        {
            _commentService = commentService;
            _taskService = taskService;
        }

       
        public IActionResult CreateComment(Comment comment)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");
            string url = "/task/taskdetail?id=" + comment.TaskId;
            comment.UserId = new Guid(HttpContext.Session.GetString("LoginId"));
           
            ResponseModel response = _commentService.AddComment(comment);
            if (response.Success)
            {
                HttpContext.Session.SetString("CommentMessage", response.Message);
                return Redirect(url);
            }
            HttpContext.Session.SetString("CommentMessage", response.Message);
            return Redirect(url);
        }

        public IActionResult DeleteComment(Guid commentId)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            _commentService.DeleteComment(commentId);
            return View();
        }


    }
}
