using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.ViewModels.UserViewModels
{
    public class ResetPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Password { get; set; }
        [Required]
        public string MailCode { get; set; }
    }
}
