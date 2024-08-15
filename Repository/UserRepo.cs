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

        #region GetUserById
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public User GetUserById(int userId)
        {
            try
            {
                User newUser = null;

                using (var con = CreateConnection())
                {
                    string selectQuery = $"SELECT  * FROM Users WHERE Id = {userId} ";
                    var selectCommand = new SqlCommand(selectQuery, con);
                    using (SqlDataReader reader1 = selectCommand.ExecuteReader())
                    {
                        if (reader1.Read())
                        {
                            newUser = new User
                            {
                                Id = reader1.GetInt32(0),
                                FirstName = reader1.GetString(1),
                                LastName = reader1.GetString(2),
                                Email = reader1.GetString(3),
                                Roles = reader1.GetString(4)
                            };
                        }
                    }
                    return newUser;
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

        #region AddUser
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>

        public User AddUser(User user)
        {
            try
            {
                User newUser = null;
                using (var con = CreateConnection())
                {
                    var command = new SqlCommand($"INSERT INTO Users (FirstName, LastName, Email, Roles)  VALUES ('" + user.FirstName + "','" + user.LastName + "','" + user.Email + "','" + user.Roles + "') SELECT SCOPE_IDENTITY()", con);
                    var CreatedUserId = Convert.ToInt32(command.ExecuteScalar());
                    if(CreatedUserId > 0)
                    {
                        newUser = GetUserById(CreatedUserId);

                    }
                    return newUser;
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

        #region UpdateUser
        /// <summary>
        /// Description :  Method to open Database Connection  
        /// Date Modified :9 Aug 2024
        /// </summary>
        public User  UpdateUser(User user)
        {
            try
            {
                User updatedUser = null;
                using (var con = CreateConnection())
                {
                    var command = new SqlCommand($"UPDATE Users SET FirstName = '" + user.FirstName + "', LastName = '" + user.LastName + "', Email = '" + user.Email + "', Roles ='" + user.Roles + $"' WHERE Id = {user.Id}", con);
                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        updatedUser = GetUserById(user.Id);

                    }
                    return updatedUser;
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

        #region DeleteUser
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

      
    }
}

