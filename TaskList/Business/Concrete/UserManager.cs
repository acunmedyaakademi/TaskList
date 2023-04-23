using filmblogu.Core;
using System.Threading.Tasks;
using TaskList.Business.Abstract;
using TaskList.Core;
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
        private readonly MailKitService _mailService = new MailKitService();
        private readonly CodeGenerator _codeGenerator = new CodeGenerator();

        public UserManager(IUserDal userDal, IHttpContextAccessor accessor)
        {
            _userDal = userDal;
            _accessor = accessor;
        }

        public bool Register(AddUser addUser)
        {
            if (CheckString.Check(addUser.Email) && CheckString.Check(addUser.Password) && CheckString.Check(addUser.Name))
            {
                User user = _userDal.GetUserByMail(addUser.Email);
                if (user == null)
                {
                    if (_userDal.AddUser(addUser))
                    {
                        string code = _codeGenerator.RandomPassword(6);
                        if (_userDal.SetMailCode(addUser.Email, code))
                            return _mailService.SendMailPassword(addUser.Email, code);
                    }
                }
            }
            return false;
        }

        public bool ControlIsEmailConfirmed(string email)
        {
            if (CheckString.Check(email))
            {
                return _userDal.ControlIsEmailConfirmed(email);
            }
            return false;
        }

        public bool ControlMailTime(string email)
        {
            if (!CheckString.Check(email))
            {
                return false;
            }
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
            if (CheckString.Check(loginUser.Email) && CheckString.Check(loginUser.Password))
            {
                return _userDal.Login(loginUser);
            }
            return null;
        }

        public bool ResetPassword(ResetPassword resetPassword)
        {
            if (CheckString.Check(resetPassword.Email) && CheckString.Check(resetPassword.Password) && CheckString.Check(resetPassword.MailCode))

                return _userDal.ResetPassword(resetPassword);

            return false;
        }

        public bool SendMailCode(string Email)
        {
            if (CheckString.Check(Email))
                if (ControlMailTime(Email))
                {
                    string code = _codeGenerator.RandomPassword(6);
                    _mailService.SendMailPassword(Email, code);
                    return true;
                }
            return false;
        }

    }
}
