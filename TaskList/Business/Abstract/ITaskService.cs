namespace TaskList.Business.Abstract
{
    public interface ITaskService
    {
        bool ControlUndoneTask(Guid AssignerId, Guid AssingedById);
    }
}
