using TaskList.Models;
using TaskList.Models.ViewModels;
using Task = TaskList.Models.Task;

namespace TaskList.Interfaces
{
    public interface ITaskDal
    {
        bool AddTask(Task task);

        bool UpdateTask(Task task);

        bool DoneTask(Guid TaskId);

        bool DeleteTask(Guid TaskId);

        List<JoinedTask> GetUsersTasks(Guid id);

        List<JoinedTask> GetGivenTasks(Guid id);

        List<JoinedTask> GetAllTasks();

        bool ControlUndoneTask(Guid AssingedById);

        Task GetTask(Guid TaskId);
    }
}
