using System.Data;
using Task_Mangement.Models;

namespace Task_Mangement.Repository
{
    public interface IUser
    {
        DataTable GetAllUsers();
        DataTable GetUserById(int userId);
        int AddUser(User user);
        int UpdateUser(User user);
        int DeleteUser(int userId);

        DataTable GetLatestUser();
    }

}
