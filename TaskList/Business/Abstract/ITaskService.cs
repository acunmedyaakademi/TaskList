using TaskList.Models.ViewModels;
using Task = TaskList.Models.Task;

namespace TaskList.Business.Abstract
{
    public interface ITaskService
    {
        bool AddTask(Task task);

        bool UpdateTask(Task task);

        bool DoneTask(Guid TaskId);

        bool DeleteTask(Guid TaskId);

        List<JoinedTask> GetUsersTasks(Guid id);

        List<JoinedTask> GetGivenTasks(Guid id);

        List<JoinedTask> GetAllTasks();

        bool ControlUndoneTask(Guid AssingedById);
    }
}
