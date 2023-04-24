using TaskList.Models.ViewModels.CommentViewModels;

namespace TaskList.Models.ViewModels.TaskViewModels
{
    public class TaskDetailContainer
    {
        public JoinedTask Task { get; set; }
        public List<JoinedComment>? Comments { get; set; }
    }
}
