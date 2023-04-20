namespace TaskList.Interfaces
{
    public interface ITasks
    {
        bool AddTask(Task task);

        bool UpdateTask(Task task);

        bool DoneTask(Guid TaskId);

        bool DeleteTask(Guid TaskId);

        bool ControlUndoneTask(Guid AssignerId, Guid AssingedById);


    }
}
