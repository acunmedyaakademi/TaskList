using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.ViewModels.UserViewModels
{
    public class ForgetPassword
    {
        [Required]
        public string Email { get; set; }
    }
}
