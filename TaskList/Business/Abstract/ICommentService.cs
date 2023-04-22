using TaskList.Models.ViewModels.CommentViewModels;
using TaskList.Models;

namespace TaskList.Business.Abstract
{
    public interface ICommentService
    {
        List<JoinedComment> GetComments(Guid TaskId);

        bool DeleteComment(Guid CommentId);

        bool AddComment(Comment comment);

        Comment? GetComment(Guid UserId, Guid TaskId);

    }
}
