using System.Threading.Tasks;
using Task_Mangement.Models;

namespace Task_Mangement.Task_Management_Test.TestHelper
{
    public static class TaskHelper
    {
        public static Tasks CreateTask()
        {
            return  new Tasks { Title = "Test Task", Description = "Description", Status = "Pending", Priority = "High", AssignedTo = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };

        }
        public static Tasks UpdateTask() {
            return new Tasks { Id=72, Title = "Test Task", Description = "Description", Status = "Pending", Priority = "High", AssignedTo = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };

        }
    }
}
