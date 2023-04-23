using TaskList.Models.ViewModels.UserViewModels;
using TaskList.Models;
using TaskList.Models.ViewModels;
using Microsoft.AspNetCore.Components.Web;

namespace TaskList.Business.Abstract
{
    public interface IUserService
    {
        SessionModel? Login(LoginUser loginUser);

        ResponseModel Register(AddUser addUser);

        User GetUserById(Guid id);

        User GetUserByMail(string Email);

        ResponseModel ResetPassword(ResetPassword resetPassword);

        ResponseModel SendMailCode(string Email);

        bool ControlMailTime(string email);

        bool ControlIsEmailConfirmed(string email);

        bool ConfirmMail(string email, string mailCode);

    }
}
