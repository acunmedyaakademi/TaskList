using TaskList.Models.ViewModels.UserViewModels;

namespace TaskList.Models.ViewModels
{
    public class DashboardContainer
    { 
        public List<JoinedTask>? UserTasks { get; set; }
        public List<Report>? Reports { get; set; }
        public List<JoinedTask>? AllTasks{ get; set; }
    }
}
