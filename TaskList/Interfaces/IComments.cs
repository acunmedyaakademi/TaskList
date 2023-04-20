using System.Threading.Tasks;
using TaskList.Models;

namespace TaskList.Interfaces
{
    public interface IComments
    {

        List<Comment> GetAcceptedComments(string CommentId);

        bool AcceptComment(string CommentId);

        bool DeleteComment(string CommentId);

        bool ControUnacceptedComment(string UserId);

    }
}
