using System.Data.SqlClient;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels.TaskViewModels;
using TaskList.Models.ViewModels.UserViewModels;

namespace TaskList.DataAccess
{
    public class UserDal : IUsers
    {
        public bool AddUser(AddUser addUser)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "INSERT INTO ADO_Users (id, name, email, password, mail_code, mail_send_date, mail_confirmed, created_on, is_active) VALUES (@id, @name, @email, @password, @mailCode, @mailSendDate, @mailConfirmed, @createdOn, @isActive)",
                            connection);

                    command.Parameters.AddWithValue("@id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@name", addUser.Name);
                    command.Parameters.AddWithValue("@email", addUser.Email);
                    command.Parameters.AddWithValue("@password", addUser.Password);
                    //command.Parameters.AddWithValue("@mailCode", addUser.Name);
                    //command.Parameters.AddWithValue("@mailSendDate", addUser.Name);
                    command.Parameters.AddWithValue("@mailConfirmed", false);
                    command.Parameters.AddWithValue("@createdOn", DateTime.Now);
                    command.Parameters.AddWithValue("@isActive", true);

                    command.ExecuteNonQuery();

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }

            }

        }

        public TaskListModel GetGivenTasks(string id)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByMail(string Email)
        {
            throw new NotImplementedException();
        }

        public TaskListModel GetUsersTasks(string id)
        {
            throw new NotImplementedException();
        }

        public bool IsAdmin(string id)
        {
            throw new NotImplementedException();
        }

        public bool Login(LoginUser loginUser)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(string id)
        {
            throw new NotImplementedException();
        }
    }
}
