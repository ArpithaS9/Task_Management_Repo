using System.Data;
using Task_Mangement.Models;

namespace Task_Mangement.Repository
{
    public interface IUser
    {
        DataTable GetAllUsers();
        User GetUserById(int userId);
        User AddUser(User user);
        User UpdateUser(User user);
        int DeleteUser(int userId);
    }

}
