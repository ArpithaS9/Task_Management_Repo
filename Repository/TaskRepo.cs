using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
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

        public DataTable GetTaskById(int taskId)

        {
            try
            {
                using (var con = CreateConnection())

                {
                    var command = new SqlCommand($"SELECT * FROM Tasks WHERE Id ={taskId} ", con);
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

        #region AddTask
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public int AddTask(Tasks task)

        {
            try
            {  
               using (var con = CreateConnection())

               {
                var sqlquery = "INSERT INTO Tasks(Title, Description, Status, Priority, AssignedTo, CreatedAt, UpdatedAt) VALUES('" + task.Title + "','" + task.Description + "','" + task.Status + "','" + task.Priority + "'," + task.AssignedTo + ",'" + task.CreatedAt + "','" + task.UpdatedAt + "')";
                var command = new SqlCommand(sqlquery, con);
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

        #region UpdateTask
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public int UpdateTask(Tasks task)

        {
            try
            {
                using (var con = CreateConnection())

                {
                    var command = new SqlCommand($"UPDATE Tasks SET Title = '" + task.Title + "', Description = '" + task.Description + "', Status = '" + task.Status + "', Priority = '" + task.Priority + "', " + "AssignedTo = '" + task.AssignedTo + "', CreatedAt = '" + task.CreatedAt + "', UpdatedAt = '" + task.UpdatedAt + $"'  WHERE Id = {task.Id}", con);
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

        #region GetLatesttask
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public DataTable GetLatesttask()
        {
            using (var con = CreateConnection())
            {
                var command1 = new SqlCommand($"Select top 1 * from Tasks Order by Id desc", con);
                var reader1 = command1.ExecuteReader();
                DataTable datatable = new DataTable();
                datatable.Load(reader1);
                return datatable;
            }
        }

        #endregion
    }


}
