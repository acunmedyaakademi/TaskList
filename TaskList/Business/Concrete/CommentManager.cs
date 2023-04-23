using TaskList.Business.Abstract;
using TaskList.Core;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels.CommentViewModels;

namespace TaskList.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        readonly ICommentDal _commentDal;
        readonly IHttpContextAccessor _accessor;

        public CommentManager(ICommentDal commentDal, IHttpContextAccessor accessor)
        {
            _commentDal = commentDal;
            _accessor = accessor;
        }

        public bool AddComment(Comment comment)
        {
            if (_accessor.HttpContext.Session.GetString("LoginId") == comment.UserId.ToString())
            {
                if (CheckString.Check(comment.TheComment))
                    return _commentDal.AddComment(comment);
                return false;
            }
            return false;

        }

        public bool DeleteComment(Guid CommentId)
        {
            Comment comment = _commentDal.GetCommentbyId(CommentId);
            if (comment == null) return false;
            if (_accessor.HttpContext.Session.GetString("LoginId") == comment.UserId.ToString())
            {
                return _commentDal.DeleteComment(CommentId);
            }
            return false;
        }

        public Comment? GetComment(Guid UserId, Guid TaskId)
        {
            return _commentDal.GetComment(UserId, TaskId);
        }

        public List<JoinedComment> GetComments(Guid TaskId)
        {
            return _commentDal.GetComments(TaskId);
        }
    }
}
