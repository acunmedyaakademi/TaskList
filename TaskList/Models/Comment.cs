using System.ComponentModel.DataAnnotations;

namespace TaskList.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }

        [MaxLength(250)]
        [MinLength(2)]
        [Required]
        public string TheComment { get; set; }
    }
}
