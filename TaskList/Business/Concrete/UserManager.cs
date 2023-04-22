using TaskList.Business.Abstract;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels.UserViewModels;

namespace TaskList.Business.Concrete
{
    public class UserManager : IUserService
    {
        readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public bool AddUser(AddUser addUser)
        {
            _userDal.AddUser(addUser);
            return true;
        }

        public bool ControlIsEmailConfirmed(string email)
        {
            throw new NotImplementedException();
        }

        public bool ControlIsMailCodeExpired(string mail)
        {
            throw new NotImplementedException();
        }

        public bool ControlResetPasswordTime(string email)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(string id)
        {
            return _userDal.GetUserById(id);
        }

        public User GetUserByMail(string Email)
        {
            return _userDal.GetUserByMail(Email);
        }

        public SessionModel? Login(LoginUser loginUser)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(ResetPassword resetPassword)
        {
            _userDal.ResetPassword(resetPassword);
            return true;
        }
    }
}
