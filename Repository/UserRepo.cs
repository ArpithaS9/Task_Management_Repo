using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Task_Mangement.Models;

namespace Task_Mangement.Repository
{
    public class UserRepo: IUser
    {
        
    private readonly string _connectionString;

        public UserRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        #region GetAllUsers
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public DataTable GetAllUsers()
        {
            try
            {
                using (var con = CreateConnection())
                {
                    var command = new SqlCommand("SELECT * FROM Users", con);
                    var reader = command.ExecuteReader();
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

        #region GetAllUsers
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public DataTable GetUserById(int userId)
        {
            try
            {
                using (var con = CreateConnection())
                {
                    var command = new SqlCommand($"SELECT * FROM Users WHERE Id = {userId}", con);
                    var reader = command.ExecuteReader();
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

        #region GetAllUsers
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>

        public int AddUser(User user)
        {
            try
            {
                using (var con = CreateConnection())
                {
                    User user1 = new User();
                    var command = new SqlCommand($"INSERT INTO Users (FirstName, LastName, Email, Roles)  VALUES ('" + user.FirstName + "','" + user.LastName + "','" + user.Email + "','" + user.Roles + "')", con);
                    var rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (SqlException ex)
            {
                throw new CustomException("Email should be unique");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        #endregion
        #region GetAllUsers
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public int  UpdateUser(User user)
        {
            try
            {
                using (var con = CreateConnection())
                {
                    var command = new SqlCommand($"UPDATE Users SET FirstName = '" + user.FirstName + "', LastName = '" + user.LastName + "', Email = '" + user.Email + "', Roles ='" + user.Roles + $"' WHERE Id = {user.Id}", con);
                    var rowsAffetcted = command.ExecuteNonQuery();
                    return rowsAffetcted;
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

        #region GetAllUsers
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public int DeleteUser(int userId)
        {
            try
            {
                using (var con = CreateConnection())
                {
                    var command = new SqlCommand($"DELETE FROM Users WHERE Id = {userId}", con);
                    var rowsAffetcted = command.ExecuteNonQuery();
                    return rowsAffetcted;
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

        #region GetAllUsers
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public DataTable GetLatestUser()
        {
            try
            {
                using (var con = CreateConnection())
                {

                    var command1 = new SqlCommand($"Select top 1 * from Users Order by Id desc", con);
                    var reader1 = command1.ExecuteReader();
                    DataTable datatable = new DataTable();
                    datatable.Load(reader1);
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
    }
}

