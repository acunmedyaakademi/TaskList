using TaskList.Models;
using TaskList.Models.ViewModels;
using TaskList.Models.ViewModels.UserViewModels;
using Task = TaskList.Models.Task;

namespace TaskList.Interfaces
{
    public interface ITaskDal
    {
        bool AddTask(Task task);

        bool UpdateTask(Task task);

        bool DoneTask(Guid TaskId);

        bool UndoneTask(Guid TaskId);

        bool DeleteTask(Guid TaskId);

        List<JoinedTask> GetUsersTasks(Guid id);

        List<JoinedTask> GetGivenTasks(Guid id);

        List<JoinedTask> GetAllTasks();

        bool ControlUndoneTask(Guid AssingedById);

        JoinedTask GetTask(Guid TaskId);

        Task GetTaskById(Guid taskId);

        List<Report>? GetReports();

        public bool ControlUndoneTaskForUpdate(Guid AssingedById, Guid taskId)
;
    }
}
