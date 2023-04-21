namespace TaskList.Models.ViewModels
{
    public class TaskListModel
    { 
        public List<JoinedTask> UserTasks { get; set; }
        public List<JoinedTask> GivenTasks { get; set; }
        public List<Comment> Comments{ get; set; }
    }
}
