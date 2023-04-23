using System.Data.SqlClient;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels;
using TaskList.Models.ViewModels.UserViewModels;

namespace TaskList.DataAccess.Concrete
{
    public class UserDal : IUserDal //TODO: TEST EDİLECEK
    {
        public bool AddUser(AddUser addUser) //TODO: DÜZENLENECEK
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "INSERT INTO users (id, name, email, password, mail_code, mail_send_date, mail_confirmed, created_on, is_active) VALUES (@id, @name, @email, @password, @mailCode, @mailSendDate, @mailConfirmed, @createdOn, @isActive)",
                            connection);

                    command.Parameters.AddWithValue("@id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@name", addUser.Name);
                    command.Parameters.AddWithValue("@email", addUser.Email);
                    command.Parameters.AddWithValue("@password", addUser.Password);
                    command.Parameters.AddWithValue("@mailCode", "nul");
                    command.Parameters.AddWithValue("@mailSendDate", DateTime.Now);
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

        public bool ControlIsEmailConfirmed(string email)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "select id, name, email, password, mail_code, mail_send_date, mail_confirmed, created_on, is_active from users where email = @email and mail_confirmed = true",
                            connection);

                    command.Parameters.AddWithValue("@email", email);

                    var reader = command.ExecuteReader();

                    reader.Read();
                    if(reader.HasRows)
                    {
                        return true;
                    }
                    return false;

                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public DateTime? GetMailTime(string email)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "select mail_send_date, mail_confirmed, is_active from users where email = @email and is_acitve = true",
                            connection);

                    command.Parameters.AddWithValue("@email", email);

                    var reader = command.ExecuteReader();

                    reader.Read();
                    if(reader.HasRows)
                    {
                        return reader.GetDateTime(0);
                    }

                    return null;

                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public User GetUserById(string id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    User user = new();

                    connection.Open();

                    var command = new SqlCommand(
                            "select id, name, email, password, mail_code, mail_send_date, mail_confirmed, created_on, is_active from users where id = @id",
                            connection);

                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();

                    reader.Read();
                    user.Id = reader.GetGuid(0);
                    user.Name =reader.GetString(1);
                    user.Email = reader.GetString(2);
                    user.Password = reader.GetString(3);
                    user.MailCode = reader.GetString(4);
                    user.MailSendDate = reader.GetDateTime(5);
                    user.MailConfirmed = reader.GetBoolean(6);
                    user.CreatedOn= reader.GetDateTime(7);
                    user.IsActive= reader.GetBoolean(8);

                    return user;

                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public User GetUserByMail(string Email)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    User user = new();

                    connection.Open();

                    var command = new SqlCommand(
                            "select id, name, email, password, mail_code, mail_send_date, mail_confirmed, created_on, is_active from users where email = @email",
                            connection);

                    command.Parameters.AddWithValue("@email", Email);

                    var reader = command.ExecuteReader();

                    reader.Read();
                    user.Id = reader.GetGuid(0);
                    user.Name = reader.GetString(1);
                    user.Email = reader.GetString(2);
                    user.Password = reader.GetString(3);
                    user.MailCode = reader.GetString(4);
                    user.MailSendDate = reader.GetDateTime(5);
                    user.MailConfirmed = reader.GetBoolean(6);
                    user.CreatedOn = reader.GetDateTime(7);
                    user.IsActive = reader.GetBoolean(8);

                    return user;

                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }


        public SessionModel? Login(LoginUser loginUser)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    User user = new();
                    connection.Open();

                    var command = new SqlCommand(
                            "select id, name, email, mail_confirmed from users where email = @email and password = @password and is_active = 1",connection); 
                    command.Parameters.AddWithValue("@password", loginUser.Password);
                    command.Parameters.AddWithValue("@email", loginUser.Email);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        SessionModel model = new();
                        model.Id = reader.GetGuid(0);
                        model.Name = reader.GetString(1);
                        model.Email = reader.GetString(2);
                        model.MailConfirmed = reader.GetBoolean(3);
                        return model;
                    }

                    return null;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public bool ResetPassword(ResetPassword resetPassword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue)) //TODO:  sayackonulacak
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("UPDATE users SET password = @password WHERE email = @email and mail_code = @mail_Code",connection);

                    command.Parameters.AddWithValue("@email", resetPassword.Email);
                    command.Parameters.AddWithValue("@mail_Code", resetPassword.MailCode);
                    command.Parameters.AddWithValue("@password", resetPassword.Password);

                    int a = command.ExecuteNonQuery();

                    if (a==0)
                    {
                        return false;
                    }

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool SetMailCode(string email, string mailCode)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue)) //TODO:  sayackonulacak
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("UPDATE users SET mail_code = @mailCode, mail_send_date = @mailDate  WHERE email = @email", connection);

                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@mailCode",mailCode);
                    command.Parameters.AddWithValue("@mailDate", DateTime.Now);

                    int a = command.ExecuteNonQuery();

                    if (a == 0)
                    {
                        return false;
                    }

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
