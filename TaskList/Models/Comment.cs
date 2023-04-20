namespace TaskList.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public string TaskId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
