using TaskList.Interfaces;
using TaskList.Models;

namespace TaskList.DataAccess
{
    public class CommentDal : IComments
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
