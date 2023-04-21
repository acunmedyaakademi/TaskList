namespace TaskList.Business.Abstract
{
    public interface IUserService
    {
        bool ControlResetPasswordTime(string email);

        bool ControlIsMailCodeExpired(string mail/*buraya id de gelebilir duruma göre*/);

        bool ControlIsEmailConfirmed(string email);

    }
}
