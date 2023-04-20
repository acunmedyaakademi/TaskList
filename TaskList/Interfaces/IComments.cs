using System.Threading.Tasks;
using TaskList.Models;

namespace TaskList.Interfaces
{
    public interface IComments
    {

        List<Comment> GetComments(Guid TaskId);

        bool DeleteComment(Guid CommentId);

    }
}
