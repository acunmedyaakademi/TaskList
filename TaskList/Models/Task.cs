namespace TaskList.Models
{
    public class Task
    {
        public string Id { get; set; }
        public string AssingerId { get; set; }
        public string AssignedById { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
