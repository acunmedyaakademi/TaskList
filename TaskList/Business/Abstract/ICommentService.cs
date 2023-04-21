namespace TaskList.Business.Abstract
{
    public interface ICommentService
    {
        bool ControlCommentDate();
        bool ControlId(Guid id);
    }
}
