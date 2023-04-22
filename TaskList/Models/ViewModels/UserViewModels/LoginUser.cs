using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.ViewModels.UserViewModels
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Password { get; set; }

        public bool MailConfirmed{ get; set; }

    }
}
