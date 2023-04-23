using TaskList.Models;
using TaskList.Models.ViewModels.UserViewModels;

namespace TaskList.Interfaces
{
    public interface IUserDal
    {
        SessionModel? Login(LoginUser loginUser);

        List<GetUserModel> GetUsers();

        bool AddUser(AddUser addUser);

        User GetUserById(Guid id);

        User GetUserByMail(string Email);

        bool ResetPassword(ResetPassword resetPassword);

        bool SetMailCode(string email, string mailCode);

        DateTime? GetMailTime(string email);

        bool ControlIsEmailConfirmed(string email);

        bool ConfirmMail(string email, string mailCode);

    }
}
