using Task_Mangement.Models;

namespace Task_Mangement.Task_Management_Test.TestHelper
{
    public static class UserHelper
    {

        public static User CreateUser(string email) {
            return new User { FirstName = "Amul", LastName = "Rai", Email = email, Roles = "Manager" };
        }
        public static User UpdateUser()
        {

            return new User {Id=4, FirstName = "Amul", LastName = "Rai", Email = "Amul123@gmail.com", Roles = "Manager" };
        }
    }
}
