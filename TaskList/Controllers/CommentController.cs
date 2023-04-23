using Microsoft.AspNetCore.Mvc;
using TaskList.Business.Abstract;
using TaskList.Models;

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

            _commentService.AddComment(comment);
            return View();
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
