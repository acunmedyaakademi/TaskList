using TaskList.Models.ViewModels.UserViewModels;

namespace TaskList.Models.ViewModels.TaskViewModels
{
    public class UpdateTaskModel
    {
        public List<GetUserModel> Users { get; set; }
        public JoinedTask task { get; set; }
    }
}
