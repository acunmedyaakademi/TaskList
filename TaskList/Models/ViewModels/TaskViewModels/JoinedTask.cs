namespace TaskList.Models.ViewModels
{
    public class JoinedTask
    {
        public Guid Id { get; set; }
        public string AssingerName { get; set; }
        public string AssignedByName { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
