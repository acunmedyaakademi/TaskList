using TaskList.Models.ViewModels;

namespace TaskList.Interfaces
{
    public interface ITaskDal
    {
        bool AddTask(Task task);

        bool UpdateTask(Task task);

        bool DoneTask(Guid TaskId);

        bool DeleteTask(Guid TaskId);

        TaskListModel GetUsersTasks(Guid id);

        TaskListModel GetGivenTasks(Guid id);

        TaskListModel GetAllTasks();

        bool PostTask(Task task);
    }
}
