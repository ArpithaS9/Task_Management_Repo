using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Threading.Tasks;
using Task_Mangement.Models;

namespace Task_Mangement.Repository
{
    public class TaskRepo : ITask
    {

        private readonly string _connectionString;

        public TaskRepo(IConfiguration configuration)

        {

            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        #region CreateConnection
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        private SqlConnection CreateConnection()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        #endregion

        #region GetAllTasks
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public DataTable  GetAllTasks()

        {

            try
            {
                using (var con = CreateConnection())

                {
                    var command = new SqlCommand("SELECT * FROM Tasks", con);
                    SqlDataReader reader = command.ExecuteReader();
                    var datatable = new DataTable();
                    datatable.Load(reader);
                    return datatable;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

         }
        #endregion

        #region GetTaskById
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>

        public Tasks GetTaskById(int taskId)

        {
            try
            {
                Tasks task = null;

                using (var con = CreateConnection())

                {
                    string selectQuery = $"SELECT  * FROM Tasks WHERE Id = {taskId} ";
                    var selectCommand = new SqlCommand(selectQuery, con);
                    using (SqlDataReader reader1 = selectCommand.ExecuteReader())
                    {
                        if (reader1.Read())
                        {
                            task = new Tasks
                            {
                                Id = reader1.GetInt32(0),
                                Title = reader1.GetString(1),
                                Description = reader1.GetString(2),
                                Status = reader1.GetString(3),
                                Priority = reader1.GetString(4),
                                AssignedTo = reader1.IsDBNull(5) ? (int?)null : reader1.GetInt32(5),
                                CreatedAt = reader1.GetDateTime(6),
                                UpdatedAt = reader1.GetDateTime(7),
                            };
                        }
                    }
                    return task;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }
        #endregion

        #region AddTask
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public Tasks AddTask(Tasks task)

        {
            try
            {
                Tasks newTask = new Tasks();

                using (var con = CreateConnection())

                {

                    //// DateTime.TryParse(task.CreatedAt.ToString(),out DateTime cd);
                    //    //var sqlquery = "INSERT INTO Tasks(Title, Description, Status, Priority, AssignedTo, CreatedAt, UpdatedAt) VALUES('" + task.Title + "','" + task.Description + "','" + task.Status + "','" + task.Priority + "'," + task.AssignedTo + ", CAST('"+task.CreatedAt.ToString()+"' as DateTime) ,  CAST('"+task.UpdatedAt.ToString() + "' as DateTime))";
                    //    var sqlquery = "INSERT INTO Tasks(Title, Description, Status, Priority, AssignedTo, CreatedAt, UpdatedAt) VALUES('" + task.Title + "','" + task.Description + "','" + task.Status + "','" + task.Priority + "'," + task.AssignedTo + ","+$" { task.CreatedAt},{task.UpdatedAt})";
                    //    var command = new SqlCommand(sqlquery, con);
                    //    rowsAffected = command.ExecuteNonQuery();   
                    //    return rowsAffected;
                    var sqlquery = "INSERT INTO Tasks (Title, Description, Status, Priority, AssignedTo, CreatedAt, UpdatedAt) " + "VALUES (@Title, @Description, @Status, @Priority, @AssignedTo, @CreatedAt, @UpdatedAt) SELECT SCOPE_IDENTITY()";
                    using (var command = new SqlCommand(sqlquery, con))
                    {
                        command.Parameters.AddWithValue("@Title", task.Title);
                        command.Parameters.AddWithValue("@Description", task.Description);
                        command.Parameters.AddWithValue("@Status", task.Status);
                        command.Parameters.AddWithValue("@Priority", task.Priority);
                        command.Parameters.AddWithValue("@AssignedTo", task.AssignedTo);
                        command.Parameters.AddWithValue("@CreatedAt", task.CreatedAt);
                        command.Parameters.AddWithValue("@UpdatedAt", task.UpdatedAt);

                        int createdTaskId = Convert.ToInt32(command.ExecuteScalar());
                        if (createdTaskId > 0)
                        {
                            newTask = GetTaskById(createdTaskId);
                        }

                    }
                }
                return newTask;

            }



            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
        #endregion

        #region UpdateTask
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public Tasks UpdateTask(Tasks task)

        {
            try
            {
                Tasks updatedTask = null;
                using (var con = CreateConnection())

                {
                   // var command = new SqlCommand($"UPDATE Tasks SET Title = '" + task.Title + "', Description = '" + task.Description + "', Status = '" + task.Status + "', Priority = '" + task.Priority + "', " + "AssignedTo = '" + task.AssignedTo + "', CreatedAt = '" + task.CreatedAt + "', UpdatedAt = '" + task.UpdatedAt + $"'  WHERE Id = {task.Id}", con);
                    //int rowsAffected = command.ExecuteNonQuery();
                    //return rowsAffected;

                    
                    var sqlquery = "Update tasks set Title = @Title,Description = @Description, Status = @Status, Priority = @Priority, AssignedTo = @AssignedTo, CreatedAt = @CreatedAt,UpdatedAt= @UpdatedAt where Id = @Id";
                    using (var command = new SqlCommand(sqlquery, con))
                    { 
                        command.Parameters.AddWithValue("@Id", task.Id);
                        command.Parameters.AddWithValue("@Title", task.Title);
                        command.Parameters.AddWithValue("@Description", task.Description);
                        command.Parameters.AddWithValue("@Status", task.Status);
                        command.Parameters.AddWithValue("@Priority", task.Priority);
                        command.Parameters.AddWithValue("@AssignedTo", task.AssignedTo);
                        command.Parameters.AddWithValue("@CreatedAt", task.CreatedAt);
                        command.Parameters.AddWithValue("@UpdatedAt", task.UpdatedAt);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            updatedTask = GetTaskById(task.Id);
                        }


                    }
                    return updatedTask;
                }
            }
            
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
        #endregion

        #region DeleteTask
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public int DeleteTask(int taskId)

        {
            try
            {
                using (var con = CreateConnection())

                {
                    var command = new SqlCommand($"DELETE FROM Tasks WHERE Id = {taskId}", con);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
        #endregion


    }


}
