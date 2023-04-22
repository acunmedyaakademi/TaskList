using TaskList.Business.Abstract;
using TaskList.Interfaces;
using TaskList.Models.ViewModels;
using Task = TaskList.Models.Task;

namespace TaskList.Business.Concrete
{
    public class TaskManager : ITaskService
    {
        readonly ITaskDal _taskDal;

        public TaskManager(ITaskDal taskDal)
        {
            _taskDal = taskDal;
        }


        public bool AddTask(Task task)
        {
            _taskDal.AddTask(task);
            return true;
        }

        public bool ControlUndoneTask(Guid AssignerId, Guid AssingedById)
        {
            throw new NotImplementedException();
        }

        public bool ControlUndoneTask(Guid AssingedById)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTask(Guid TaskId)
        {
            _taskDal.DeleteTask(TaskId);
            return true;
        }

        public bool DoneTask(Guid TaskId)
        {
            _taskDal.DoneTask(TaskId);
            return true;
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
            _taskDal.UpdateTask(task);
            return true;
        }
    }
}
