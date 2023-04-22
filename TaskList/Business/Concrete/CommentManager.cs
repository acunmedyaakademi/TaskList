using TaskList.Business.Abstract;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels.CommentViewModels;

namespace TaskList.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public bool AddComment(Comment comment)
        {
            _commentDal.AddComment(comment);
            return true;
        }


        public bool DeleteComment(Guid CommentId)
        {
            _commentDal.DeleteComment(CommentId);
            return true;
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
