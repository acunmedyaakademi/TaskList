using System.ComponentModel.Design;
using System.Data.SqlClient;
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

                    command.Parameters.AddWithValue("@Id", comment.Id);
                    command.Parameters.AddWithValue("@Comment", comment.TheComment);
                    command.Parameters.AddWithValue("@IsActive", comment.IsActive);
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
                    var command = new SqlCommand("DELETE FROM Comments WHERE ID = @id", connection);
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

        public List<JoinedComment> GetComments(Guid TaskId)
        {
            try
            {
                var commentList = new List<JoinedComment>();

                using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
                {
                    connection.Open();
                    var command = new SqlCommand("select c.id, comment, c.created_on, u.name, t.task_name, c.is_active from comments as c join users as u on u.id = u.id join tasks as t on c.task_id = t.id where t.id = @id", connection);
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

                throw;
            }
        }
    }
}
//test edilmedi