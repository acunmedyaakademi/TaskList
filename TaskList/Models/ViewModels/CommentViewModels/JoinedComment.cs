namespace TaskList.Models.ViewModels.CommentViewModels
{
    public class JoinedComment
    {

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string TaskName { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public string theComment { get; set; }

    }
}
