namespace TaskList.Interfaces
{
    public interface ITasks
    {
        bool AddTask(Task task);

        bool UpdateTask(Task task);

        bool DoneTask(string TaskId);

        bool DeleteTask(string TaskId);

        bool ControlUndoneTask(string AssignerId, string AssingerById);


    }
}
