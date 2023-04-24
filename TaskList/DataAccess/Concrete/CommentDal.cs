using NuGet.ContentModel;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels.CommentViewModels;

namespace TaskList.DataAccess.Concrete
{
    public class CommentDal : ICommentDal
    {
        public bool AddComment(Comment comment)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
                {
                    connection.Open();
                    var command = new SqlCommand("INSERT INTO [comments] (id, comment, created_on, user_id, task_id, is_active) VALUES (@Id ,@Comment, @Date, @UserId, @TaskId, @IsActive)", connection);

           
                    command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@Comment", comment.TheComment);
                    command.Parameters.AddWithValue("@IsActive", true);
                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                    command.Parameters.AddWithValue("@UserId", comment.UserId);
                    command.Parameters.AddWithValue("@TaskId", comment.TaskId);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteComment(Guid CommentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
                {
                    connection.Open();
                    var command = new SqlCommand("UPDATE Comments SET [is_active] = 0  WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", CommentId);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Comment? GetComment(Guid UserId, Guid TaskId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
                {
                    connection.Open();
                    var command = new SqlCommand("select  TOP (1) [id] , [user_id], [task_id], [created_on], [is_active], [comment] from comments where user_id = @user_id and task_id = @task_id order by created_on desc", connection);
                    command.Parameters.AddWithValue("@user_id", UserId);
                    command.Parameters.AddWithValue("@task_id", TaskId);

                    var reader = command.ExecuteReader();

                    reader.Read();
                    var commentItem = new Comment();
                    commentItem.Id = reader.GetGuid(0);
                    commentItem.UserId = reader.GetGuid(1);
                    commentItem.TaskId = reader.GetGuid(2);
                    commentItem.CreatedOn = reader.GetDateTime(3);
                    commentItem.IsActive = reader.GetBoolean(4);
                    commentItem.TheComment = reader.GetString(5);
                    return commentItem;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Comment? GetCommentbyId(Guid commentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
                {
                    connection.Open();
                    var command = new SqlCommand("select [id] , [user_id], [task_id], [created_on], [is_active], [comment] from comments where id = @commentId", connection);
                    command.Parameters.AddWithValue("@commentId", commentId);

                    var reader = command.ExecuteReader();

                    reader.Read();
                    var commentItem = new Comment();
                    commentItem.Id = reader.GetGuid(0);
                    commentItem.UserId = reader.GetGuid(1);
                    commentItem.TaskId = reader.GetGuid(2);
                    commentItem.CreatedOn = reader.GetDateTime(3);
                    commentItem.IsActive = reader.GetBoolean(4);
                    commentItem.TheComment = reader.GetString(5);
                    return commentItem;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<JoinedComment>? GetComments(Guid TaskId)
        {
            try
            {
                var commentList = new List<JoinedComment>();

                using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
                {
                    connection.Open();
                    var command = new SqlCommand("select c.id, comment, c.created_on, u.name, t.task_name, c.is_active from comments as c join users as u on u.id = c.user_id join tasks as t on c.task_id = t.id where t.id = @id and c.is_active = 1", connection);
                    command.Parameters.AddWithValue("@id", TaskId);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var commentItem = new JoinedComment();
                        commentItem.Id = reader.GetGuid(0);
                        commentItem.TheComment = reader.GetString(1);
                        commentItem.CreatedOn = reader.GetDateTime(2);
                        commentItem.UserName = reader.GetString(3);
                        commentItem.TaskName = reader.GetString(4);
                        commentItem.IsActive = reader.GetBoolean(5);

                        commentList.Add(commentItem);
                    }
                }
                return commentList;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
//test edilmedi