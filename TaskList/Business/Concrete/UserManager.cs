using filmblogu.Core;
using TaskList.Business.Abstract;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels.UserViewModels;
using TestApp.Core;

namespace TaskList.Business.Concrete
{
    public class UserManager : IUserService
    {
        readonly IUserDal _userDal;
        readonly IHttpContextAccessor _accessor;
        private readonly MailKitService _mailKitService = new MailKitService();
        private readonly CodeGenerator _codeGenerator = new CodeGenerator();

        public UserManager(IUserDal userDal, IHttpContextAccessor accessor)
        {
            _userDal = userDal;
            _accessor = accessor;
        }

        public bool AddUser(AddUser addUser)
        {   
            User user = _userDal.GetUserByMail(addUser.Email);
            if(user == null)
            {
                return _userDal.AddUser(addUser);
            }
            return false;
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

        public bool SendResetCode(string Email)
        {
            if(ControlMailTime(Email))
            {
                string code = _codeGenerator.RandomPassword(10);
                _mailKitService.SendMailPassword(Email, code);
                return true;
            }
            return false;
        }
    }
}
