using TaskList.Interfaces;

namespace TaskList.DataAccess
{
    public class TaskDal : ITasks
    {
        public bool AddTask(Task task)
        {
            throw new NotImplementedException();
        }

        public bool ControlUndoneTask(Guid AssignerId, Guid AssingedById)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTask(Guid TaskId)
        {
            throw new NotImplementedException();
        }

        public bool DoneTask(Guid TaskId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTask(Task task)
        {
            throw new NotImplementedException();
        }
    }
}
