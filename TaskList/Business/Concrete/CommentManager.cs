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


        public bool ControlCommentDate(Guid TaskId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteComment(Guid CommentId)
        {
            _commentDal.DeleteComment(CommentId);
            return true;
        }

        public List<JoinedComment> GetComments(Guid TaskId)
        {
            return _commentDal.GetComments(TaskId);
        }
    }
}
