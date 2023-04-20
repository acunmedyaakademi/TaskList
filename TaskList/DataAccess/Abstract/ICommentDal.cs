using System.Threading.Tasks;
using TaskList.Models;

namespace TaskList.Interfaces
{
    public interface ICommentDal
    {

        List<Comment> GetComments(Guid TaskId);

        bool DeleteComment(Guid CommentId);

    }
}
