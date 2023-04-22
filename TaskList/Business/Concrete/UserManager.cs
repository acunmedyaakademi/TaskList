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
            return _userDal.ControlIsEmailConfirmed(email);
        }

        public bool ControlMailTime(string email)
        {
            if (_userDal.GetMailTime(email) == null)
            {
                return false;
            }
            else
            {
                DateTime? dateTime = _userDal.GetMailTime(email);
                TimeSpan? dif = DateTime.Now - dateTime;
                if (dif?.TotalMinutes < 5)
                {
                    return false;
                }
                return true;
            }
            
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
            return _userDal.Login(loginUser);
        }

        public bool ResetPassword(ResetPassword resetPassword)
        {
            _userDal.ResetPassword(resetPassword);
            return true;
        }
    }
}
