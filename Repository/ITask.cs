using System.Data;
using Task_Mangement.Models;

namespace Task_Mangement.Repository
{
    public interface ITask
    {
        DataTable GetAllTasks();
        Tasks GetTaskById(int taskId);
        Tasks AddTask(Tasks task);
        Tasks UpdateTask(Tasks task);
        int DeleteTask(int taskId);
    }
}
