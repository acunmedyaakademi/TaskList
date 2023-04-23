using System.ComponentModel.DataAnnotations;

namespace TaskList.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public Guid AssingerId { get; set; }
        public Guid AssignedById { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskDescription { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDone { get; set; }
        public bool IsActive { get; set; }
    }
}
