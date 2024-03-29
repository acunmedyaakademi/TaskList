﻿using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaskList.Interfaces;
using TaskList.Models.ViewModels;
using TaskList.Models.ViewModels.UserViewModels;
using Task = TaskList.Models.Task;

namespace TaskList.DataAccess.Concrete
{
    public class TaskDal : ITaskDal
    {
        public bool AddTask(Task task)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "INSERT INTO Tasks (id, [assigner_id], [assigned_by_id], [task_name], [task_description], [updated_on], [created_on], [is_done], [is_active]) VALUES (@id, @assigner_id, @assigned_by_id, @task_name, @task_description, @updated_on, @created_on, @is_done, @is_active)",
                            connection);

                    command.Parameters.AddWithValue("@id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@assigner_id", task.AssingerId);
                    command.Parameters.AddWithValue("@assigned_by_id", task.AssignedById);
                    command.Parameters.AddWithValue("@task_name", task.TaskName);
                    command.Parameters.AddWithValue("@task_description", task.TaskDescription);
                    command.Parameters.AddWithValue("@updated_on", DateTime.Now);
                    command.Parameters.AddWithValue("@created_on", DateTime.Now);
                    command.Parameters.AddWithValue("@is_done", false);
                    command.Parameters.AddWithValue("@is_active", true);

                    command.ExecuteNonQuery();

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public bool ControlUndoneTask(Guid AssingedById)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "SELECT id, assigned_by_id, created_on, updated_on, is_done, is_active FROM tasks WHERE assigned_by_id = @assingedById and is_done = 0 and is_active = 1",
                            connection);
                    command.Parameters.AddWithValue("@assingedById", AssingedById);

                    var reader = command.ExecuteReader();

                    reader.Read();
                    if (reader.HasRows)
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
        public bool ControlUndoneTaskForUpdate(Guid AssingedById, Guid taskId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "SELECT id, assigned_by_id, created_on, updated_on, is_done, is_active FROM tasks WHERE assigned_by_id = @assingedById and is_done = 0 and is_active = 1 and id <> @taskId",
                            connection);
                    command.Parameters.AddWithValue("@assingedById", AssingedById);
                    command.Parameters.AddWithValue("@taskId", taskId);

                    var reader = command.ExecuteReader();

                    reader.Read();
                    if (reader.HasRows)
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

        public bool DeleteTask(Guid TaskId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
                {
                    connection.Open();
                    var command = new SqlCommand("UPDATE Tasks SET [is_active] = 0 where id = @id", connection);
                    command.Parameters.AddWithValue("@id", TaskId);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DoneTask(Guid TaskId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "UPDATE Tasks SET [is_done] = 1 where id = @id and is_active = 1 ", connection);

                    command.Parameters.AddWithValue("@id", TaskId);
                    command.ExecuteNonQuery();

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public List<JoinedTask> GetAllTasks()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    List<JoinedTask> joinedTasks = new List<JoinedTask>();
                    connection.Open();

                    var command = new SqlCommand(
                            "SELECT t.id, u1.name, u2.name, [task_name], [task_description], t.[created_on], t.[updated_on], [is_done], t.[is_active] FROM tasks as t JOIN users as u1 ON t.assigner_id = u1.id JOIN users as u2 ON t.assigned_by_id = u2.id where t.is_active = 1 order by updated_on desc",
                            connection);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        JoinedTask task = new();
                        task.Id = reader.GetGuid(0);
                        task.AssingerName = reader.GetString(1);
                        task.AssignedByName = reader.GetString(2);
                        task.TaskName = reader.GetString(3);
                        task.TaskDescription = reader.GetString(4);
                        task.CreatedOn = reader.GetDateTime(5);
                        task.UpdatedOn = reader.GetDateTime(6);
                        task.IsDone = reader.GetBoolean(7);
                        task.IsActive = reader.GetBoolean(8);
                        joinedTasks.Add(task);
                    }

                    return joinedTasks;

                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }

        public List<JoinedTask> GetGivenTasks(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    List<JoinedTask> joinedTasks = new List<JoinedTask>();
                    connection.Open();

                    var command = new SqlCommand(
                            "SELECT t.id, u1.name, u2.name, [task_name], [task_description], t.[created_on], t.[updated_on], [is_done], t.[is_active] FROM tasks as t JOIN users as u1 ON t.assigner_id = u1.id JOIN users as u2 ON t.assigned_by_id = u2.id WHERE assigner_id = @id and t.is_active = 1",
                            connection);
                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        JoinedTask task = new();
                        task.Id = reader.GetGuid(0);
                        task.AssingerName = reader.GetString(1);
                        task.AssignedByName = reader.GetString(2);
                        task.TaskName = reader.GetString(3);
                        task.TaskDescription = reader.GetString(4);
                        task.CreatedOn = reader.GetDateTime(5);
                        task.UpdatedOn = reader.GetDateTime(6);
                        task.IsDone = reader.GetBoolean(7);
                        task.IsActive = reader.GetBoolean(8);
                        joinedTasks.Add(task);
                    }

                    return joinedTasks;

                }
                catch (Exception e)
                {
                    return null;
                }

            }
        } // test edildi

        public List<Report>? GetReports()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                List<Report> reports = new List<Report>();
                try
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT u.name, COUNT(CASE WHEN is_done = 1 THEN 1 END) , COUNT(CASE WHEN is_done = 0 THEN 1 END) FROM tasks JOIN users AS u ON assigned_by_id = u.id where tasks.is_active = 1 GROUP BY u.name", connection);
                    var reader = command.ExecuteReader();
                    JoinedTask task = new();
                    while (reader.Read())
                    {
                        Report report = new Report();
                        report.Name = reader.GetString(0);
                        report.Done = reader.GetInt32(1);
                        report.Undone = reader.GetInt32(2);
                        reports.Add(report);
                    }
                    return reports;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public JoinedTask GetTask(Guid TaskId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {

                    connection.Open();

                    var command = new SqlCommand("SELECT t.id, u1.name, u2.name, [task_name], [task_description], t.[created_on], t.[updated_on], [is_done], t.[is_active] FROM tasks as t JOIN users as u1 ON t.assigner_id = u1.id JOIN users as u2 ON t.assigned_by_id = u2.id WHERE t.id = @id and t.is_active = 1", connection);
                    command.Parameters.AddWithValue("@id", TaskId);
                    var reader = command.ExecuteReader();
                    JoinedTask task = new();
                    reader.Read();
                    task.Id = reader.GetGuid(0);
                    task.AssingerName = reader.GetString(1);
                    task.AssignedByName = reader.GetString(2);
                    task.TaskName = reader.GetString(3);
                    task.TaskDescription = reader.GetString(4);
                    task.CreatedOn = reader.GetDateTime(5);
                    task.UpdatedOn = reader.GetDateTime(6);
                    task.IsDone = reader.GetBoolean(7);
                    task.IsActive = reader.GetBoolean(8);

                    
                    return task;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public Task GetTaskById(Guid taskId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "SELECT id, assigner_id, assigned_by_id, [task_name], [task_description], [created_on], [updated_on], [is_done], [is_active] FROM tasks WHERE id = @id and is_active = 1",
                            connection);
                    command.Parameters.AddWithValue("@id", taskId);

                    var reader = command.ExecuteReader();

                    reader.Read();

                    Task task = new();
                    task.Id = reader.GetGuid(0);
                    task.AssingerId = reader.GetGuid(1);
                    task.AssignedById = reader.GetGuid(2);
                    task.TaskName = reader.GetString(3);
                    task.TaskDescription = reader.GetString(4);
                    task.CreatedOn = reader.GetDateTime(5);
                    task.UpdatedOn = reader.GetDateTime(6);
                    task.IsDone = reader.GetBoolean(7);
                    task.IsActive = reader.GetBoolean(8);

                    return task;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public List<JoinedTask> GetUsersTasks(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    List<JoinedTask> joinedTasks = new List<JoinedTask>();
                    connection.Open();

                    var command = new SqlCommand(
                            "SELECT t.id, u1.name, u2.name, [task_name], [task_description], t.[created_on], t.[updated_on], [is_done], t.[is_active] FROM tasks as t JOIN users as u1 ON t.assigner_id = u1.id JOIN users as u2 ON t.assigned_by_id = u2.id WHERE assigned_by_id = @id and t.is_active = 1",
                            connection);
                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        JoinedTask task = new();
                        task.Id = reader.GetGuid(0);
                        task.AssingerName = reader.GetString(1);
                        task.AssignedByName = reader.GetString(2);
                        task.TaskName = reader.GetString(3);
                        task.TaskDescription = reader.GetString(4);
                        task.CreatedOn = reader.GetDateTime(5);
                        task.UpdatedOn = reader.GetDateTime(6);
                        task.IsDone = reader.GetBoolean(7);
                        task.IsActive = reader.GetBoolean(8);
                        joinedTasks.Add(task);
                    }
                    return joinedTasks;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public bool UndoneTask(Guid TaskId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "UPDATE Tasks SET [is_done] = 0 where id = @id and is_active = 1 ", connection);

                    command.Parameters.AddWithValue("@id", TaskId);
                    command.ExecuteNonQuery();

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public bool UpdateTask(Task task)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString.ConnectionValue))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand("UPDATE Tasks SET [assigner_id] = @assigner_id, [assigned_by_id] = @assigned_by_id, [task_name] = @task_name, [task_description] = @task_description , [updated_on] = @updated_on, [is_done] = @is_done where id = @id ", connection);

                    command.Parameters.AddWithValue("@id", task.Id);
                    command.Parameters.AddWithValue("@assigner_id", task.AssingerId);
                    command.Parameters.AddWithValue("@assigned_by_id", task.AssignedById);
                    command.Parameters.AddWithValue("@task_name", task.TaskName);
                    command.Parameters.AddWithValue("@task_description", task.TaskDescription);
                    command.Parameters.AddWithValue("@updated_on", DateTime.Now);
                    command.Parameters.AddWithValue("@is_done", task.IsDone);
                    command.ExecuteNonQuery();

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
