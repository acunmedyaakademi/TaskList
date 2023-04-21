using TaskList.Business.Abstract;

namespace TaskList.Business.Concrete
{
    public class UserManager : IUserService
    {
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
    }
}
