using System.Threading.Tasks;
using TaskList.Business.Abstract;
using TaskList.Core;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels;
using TaskList.Models.ViewModels.UserViewModels;
using TestApp.Core;
using Task = TaskList.Models.Task;

namespace TaskList.Business.Concrete
{
    public class TaskManager : ITaskService
    {
        readonly ITaskDal _taskDal;
        readonly IUserDal _userDal;
        readonly IHttpContextAccessor _accessor;
        MailKitService _mailKitService = new();


        public TaskManager(ITaskDal taskDal, IHttpContextAccessor accessor, IUserDal userDal)
        {
            _taskDal = taskDal;
            _accessor = accessor;
            _userDal = userDal;
        }

        public ResponseModel AddTask(Task task)
        {
            ResponseModel response = new();
            response.Success = false;

            if (CheckString.Check(task.TaskDescription) && CheckString.Check(task.TaskName))
            {
                Guid userId = new Guid(_accessor.HttpContext.Session.GetString("LoginId"));
                if (task.AssingerId == userId)
                {
                    if (ControlUndoneTask(task.AssignedById))
                    {
                        if (userId != task.AssignedById)
                        {
                            if (_taskDal.AddTask(task))
                            {
                                User user = _userDal.GetUserById(task.AssignedById); //todo semantik
                                response.Success = _mailKitService.SendMailPassword(user.Email, task.TaskName + "- size görev geldi");
                                return response;
                            }
                            response.Message = "Görev eklenirken bir hata oluştu";
                            return response;
                        }
                        response.Message = "Kendine Görev Veremezsin";
                        return response;
                    }
                    response.Message = "Bu kullanıcın henüz bitirmediği bir görev var, yeni görev verilemez";
                    return response;
                }
                response.Message = "onu yapma artık";
                return response;
            }
            response.Message = "özel karakter kullanılamaz";
            return response;
        }

        public bool ControlUndoneTask(Guid AssingedById)
        {
            return _taskDal.ControlUndoneTask(AssingedById);
        }

        public ResponseModel DeleteTask(Guid TaskId)
        {
            ResponseModel response = new();
            response.Success = false;

            string userName = _accessor.HttpContext.Session.GetString("LoginName");
            JoinedTask task = _taskDal.GetTask(TaskId);
            if (task.AssingerName == userName)
            {
                response.Success = _taskDal.DeleteTask(TaskId);
                return response;
            }
            response.Message = "onu yapma artık";
            return response;
        }

        public ResponseModel DoneTask(Guid TaskId)
        {
            ResponseModel response = new();
            response.Success = false;

            string userName = _accessor.HttpContext.Session.GetString("LoginName");
            JoinedTask task = _taskDal.GetTask(TaskId);
            if (task.AssignedByName == userName && task != null)
            {
                if (_taskDal.DoneTask(TaskId))
                {
                    Task theTask = _taskDal.GetTaskById(TaskId); //todo mail controol
                    User user = _userDal.GetUserById(theTask.AssignedById);
                    response.Success = _mailKitService.SendMailPassword(user.Email, task.TaskName + " " + "verdiğiniz görev bitti");
                    return response;
                }
                response.Message = "işlem gerçekleşmedi";
                return response;
            }
            response.Message = "onu yapma";
            return response;
        }

        public List<JoinedTask> GetAllTasks()
        {
            return _taskDal.GetAllTasks();
        }

        public List<JoinedTask> GetGivenTasks(Guid id)
        {
            return _taskDal.GetGivenTasks(id);
        }

        public List<Report>? GetReports()
        {
            return _taskDal.GetReports();
        }

        public JoinedTask GetTask(Guid TaskId)
        {
            return _taskDal.GetTask(TaskId);
        }

        public Task GetTaskById(Guid taskId)
        {
            return _taskDal.GetTaskById(taskId);
        }

        public List<JoinedTask> GetUsersTasks(Guid id)
        {
            return _taskDal.GetUsersTasks(id);
        }

        public ResponseModel UndoneTask(Guid TaskId)
        {
            ResponseModel response = new();
            response.Success = false;

            string userName = _accessor.HttpContext.Session.GetString("LoginName");
            JoinedTask task = _taskDal.GetTask(TaskId);
            if (task.AssignedByName == userName && task != null)
            {
                if (_taskDal.UndoneTask(TaskId))
                {
                    Task theTask = _taskDal.GetTaskById(TaskId); //todo mail controol
                    User user = _userDal.GetUserById(theTask.AssignedById);
                    //response.Success = _mailKitService.SendMailPassword(user.Email, task.TaskName + " " + "verdiğiniz görev bitti");
                    response.Success = true;
                    return response;
                }
                response.Message = "işlem gerçekleşmedi";
                return response;
            }
            response.Message = "onu yapma";
            return response;
        }

        public ResponseModel UpdateTask(Task task)
        {
            ResponseModel response = new();
            response.Success = false;

            if (CheckString.Check(task.TaskDescription) && CheckString.Check(task.TaskName))
            {
                Guid userId = new Guid(_accessor.HttpContext.Session.GetString("LoginId"));
                if (task.AssingerId == userId)
                {
                    if (_taskDal.ControlUndoneTaskForUpdate(task.AssignedById,task.Id))
                    {
                        if (userId != task.AssignedById)
                        {
                            response.Success = _taskDal.UpdateTask(task);
                            return response;
                        }
                        response.Message = "Kendine Görev Veremezsin";
                        return response;
                    }
                    response.Message = "Bu kullanıcın henüz bitirmediği bir görev var, yeni görev verilemez";
                    return response;
                }
                response.Message = "onu yapma";
                return response;
            }
            response.Message = "özel karakterler kullanılamaz";
            return response;
        }
    }
}
