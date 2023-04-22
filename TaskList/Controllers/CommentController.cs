using Microsoft.AspNetCore.Mvc;
using TaskList.Business.Abstract;
using TaskList.Models;

namespace TaskList.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        public IActionResult CreateComment(Comment comment)
        {
            _commentService.AddComment(comment);
            return View();
        }

        public IActionResult DeleteComment(Guid commentId)
        {
            _commentService.DeleteComment(commentId);
            return View();
        }
    }
}
