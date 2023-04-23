using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.ViewModels.TaskViewModels
{
    public class CreateTaskModel
    {
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskDescription { get; set; }

    }
}
