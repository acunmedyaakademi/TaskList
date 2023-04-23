using TaskList.Models.ViewModels;
using TaskList.Models.ViewModels.UserViewModels;
using Task = TaskList.Models.Task;

namespace TaskList.Business.Abstract
{
    public interface ITaskService
    {
        ResponseModel AddTask(Task task);

        ResponseModel UpdateTask(Task task);

        ResponseModel DoneTask(Guid TaskId);

        ResponseModel DeleteTask(Guid TaskId);

        List<JoinedTask> GetUsersTasks(Guid id);

        List<JoinedTask> GetGivenTasks(Guid id);

        List<JoinedTask> GetAllTasks();

        bool ControlUndoneTask(Guid AssingedById);
        List<Report>? GetReports();
    }
}
