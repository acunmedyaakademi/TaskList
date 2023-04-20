using TaskList.Interfaces;
using TaskList.Models;

namespace TaskList.DataAccess.Concrete
{
    public class CommentDal : ICommentDal
    {
        public bool DeleteComment(Guid CommentId)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetComments(Guid TaskId)
        {
            throw new NotImplementedException();
        }
    }
}
