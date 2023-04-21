using TaskList.Business.Abstract;

namespace TaskList.Business.Concrete
{
    public class TaskManager : ITaskService
    {
        public bool ControlUndoneTask(Guid AssignerId, Guid AssingedById)
        {
            throw new NotImplementedException();
        }
    }
}
