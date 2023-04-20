using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.ViewModels
{
    public class AddUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Password { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(70)]
        public string Name{ get; set; }
    }
}
