using System.Data;
using Task_Mangement.Models;

namespace Task_Mangement.Repository
{
    public interface ITask
    {
        DataTable GetAllTasks();
        DataTable GetTaskById(int taskId);
        int AddTask(Tasks task);
        int UpdateTask(Tasks task);
        int DeleteTask(int taskId);
        DataTable GetLatesttask();

    }
}
