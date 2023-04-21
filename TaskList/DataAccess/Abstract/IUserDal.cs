﻿using TaskList.Models;
using TaskList.Models.ViewModels.UserViewModels;

namespace TaskList.Interfaces
{
    public interface IUserDal
    {
        bool Login(LoginUser loginUser);

        bool AddUser(AddUser addUser);

        User GetUserById(string id);

        User GetUserByMail(string Email);

        bool ResetPassword(string id);
    }
}