using TaskList.Models;
using TaskList.Models.ViewModels;

namespace TaskList.Interfaces
{
    public interface IUser
    {
        bool Login(LoginUser loginUser);

        bool AddUser(AddUser addUser);

        User GetUserById(string id);

        User GetUserByMail(string Email);

        TaskListModel GetUsersTasks(string id);

        TaskListModel GetGivenTasks(string id);

        bool IsAdmin(string id);

        bool ResetPassword(string id);


    }
}
