using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.ViewModels
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
