﻿using TaskList.Models.ViewModels.UserViewModels;
using TaskList.Models;

namespace TaskList.Business.Abstract
{
    public interface IUserService
    {
        SessionModel? Login(LoginUser loginUser);

        bool AddUser(AddUser addUser);

        User GetUserById(string id);

        User GetUserByMail(string Email);

        bool ResetPassword(ResetPassword resetPassword);

        bool SendResetCode(string Email);

        bool ControlMailTime(string email);

        bool ControlIsEmailConfirmed(string email);

    }
}
