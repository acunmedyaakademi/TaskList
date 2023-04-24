using TaskList.Business.Abstract;
using TaskList.Core;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels;
using TaskList.Models.ViewModels.CommentViewModels;
using TestApp.Core;

namespace TaskList.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        readonly ICommentDal _commentDal;
        readonly IUserDal _userDal;
        readonly ITaskDal _taskDal;
        readonly IHttpContextAccessor _accessor;
        MailKitService _mailKitService = new();

        public CommentManager(ICommentDal commentDal, IHttpContextAccessor accessor, IUserDal userDal, ITaskDal taskDal)
        {
            _commentDal = commentDal;
            _accessor = accessor;
            _userDal = userDal;
            _taskDal = taskDal;
        }

        public ResponseModel AddComment(Comment comment)
        {
            ResponseModel response = new();
            response.Success = false;

            if (_accessor.HttpContext.Session.GetString("LoginId") == comment.UserId.ToString())
            {
                if (CheckString.Check(comment.TheComment))
                {
                    if (_commentDal.GetComment(comment.UserId,comment.TaskId).CreatedOn < DateTime.Now.AddMinutes(-3))
                    {
                        if (_commentDal.AddComment(comment))
                        {
                            User user = _userDal.GetUserById(_taskDal.GetTaskById(comment.TaskId).AssignedById);
                            response.Success = _mailKitService.SendMailPassword(user.Email, "size yorum geldi"); //todo mail send
                            response.Message = "yorumunuz gönderildi";
                            return response;
                        }
                    }                    
                    response.Message = "Yorumunuz Gönderilemedi";
                    return response;
                }
                response.Message = "Özel Karakterler Kullanılamaz";
                return response;
            }
            response.Message = "Onu yapma";
            return response;
        }

        public ResponseModel DeleteComment(Guid CommentId)
        {
            ResponseModel response = new();
            response.Success = false;
            Comment comment = _commentDal.GetCommentbyId(CommentId);

            if (_accessor.HttpContext.Session.GetString("LoginId") == comment.UserId.ToString())
            {
                response.Success = _commentDal.DeleteComment(CommentId);
                return response;
            }
            response.Message = "Yorum Silinemedi";
            return response;
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
