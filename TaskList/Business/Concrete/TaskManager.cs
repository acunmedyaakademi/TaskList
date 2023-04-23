using System.Threading.Tasks;
using TaskList.Business.Abstract;
using TaskList.Core;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels;
using Task = TaskList.Models.Task;

namespace TaskList.Business.Concrete
{
    public class TaskManager : ITaskService
    {
        readonly ITaskDal _taskDal;
        readonly IHttpContextAccessor _accessor;

        public TaskManager(ITaskDal taskDal, IHttpContextAccessor accessor)
        {
            _taskDal = taskDal;
            _accessor = accessor;
        }

        public bool AddTask(Task task)
        {
            if(CheckString.Check(task.TaskDescription) && CheckString.Check(task.TaskName))
            {
                Guid userId = new Guid(_accessor.HttpContext.Session.GetString("LoginId"));
                if (task.AssingerId == userId)
                {
                    if (ControlUndoneTask(task.AssignedById))
                    {
                        return _taskDal.AddTask(task);
                    }
                }
            }
            return false;
        }

        public bool ControlUndoneTask(Guid AssingedById)
        {
            return _taskDal.ControlUndoneTask(AssingedById); 
        }

        public bool DeleteTask(Guid TaskId)
        {
            string userName = _accessor.HttpContext.Session.GetString("LoginName");
            JoinedTask task = _taskDal.GetTask(TaskId);
            if (task.AssingerName == userName)
            {
                return _taskDal.DeleteTask(TaskId);
            }
            return false;
        }

        public bool DoneTask(Guid TaskId)
        {
            string userName = _accessor.HttpContext.Session.GetString("LoginName");
            JoinedTask task = _taskDal.GetTask(TaskId);
            if (task.AssingerName == userName)
            { 
                return _taskDal.DoneTask(TaskId);
            }
            return false;
        }

        public List<JoinedTask> GetAllTasks()
        {
            return _taskDal.GetAllTasks();
        }

        public List<JoinedTask> GetGivenTasks(Guid id)
        {
            return _taskDal.GetGivenTasks(id);
        }

        public List<JoinedTask> GetUsersTasks(Guid id)
        {
            return _taskDal.GetUsersTasks(id);
        }

        public bool UpdateTask(Task task)
        {
            if (CheckString.Check(task.TaskDescription) && CheckString.Check(task.TaskName))
            {
                Guid userId = new Guid(_accessor.HttpContext.Session.GetString("LoginId"));
                if (task.AssingerId == userId)
                {
                    return _taskDal.UpdateTask(task);
                }
            }
            return false;
        }
    }
}
