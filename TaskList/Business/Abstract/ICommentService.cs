using TaskList.Models.ViewModels.CommentViewModels;
using TaskList.Models;
using TaskList.Models.ViewModels;

namespace TaskList.Business.Abstract
{
    public interface ICommentService
    {
        List<JoinedComment> GetComments(Guid TaskId);

        ResponseModel DeleteComment(Guid CommentId);

        ResponseModel AddComment(Comment comment);

        Comment? GetComment(Guid UserId, Guid TaskId);

    }
}
