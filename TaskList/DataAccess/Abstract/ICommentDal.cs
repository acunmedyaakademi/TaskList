using System.Threading.Tasks;
using TaskList.Models;
using TaskList.Models.ViewModels.CommentViewModels;

namespace TaskList.Interfaces
{
    public interface ICommentDal
    {
        List<JoinedComment>? GetComments(Guid TaskId);

        bool DeleteComment(Guid CommentId);

        bool AddComment(Comment comment);

        Comment? GetComment(Guid UserId, Guid TaskId);


    }
}
