using Microsoft.Extensions.Configuration;
using System.Data;
using Task_Mangement.Models;
using Task_Mangement.Repository;
using Task_Mangement.Task_Management_Test.TestHelper;
using Xunit;

namespace Task_Management_Test.Repository
{
    public class TaskRepoTest
    {
        private TaskRepo _taskRepo;

        public TaskRepoTest()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", "Server=CTAADPG02J88E\\SQLEXPRESS2019;Database=TasKManagement;Trusted_Connection=true;TrustServerCertificate=true"}
            };
            var configuration = new ConfigurationBuilder()
              .AddInMemoryCollection(inMemorySettings)
              .Build();

            _taskRepo = new TaskRepo(configuration);
        }

        [Fact]
        public void GetAllTasks_ShouldReturnDataTable()
        {
            // Act
            DataTable result = _taskRepo.GetAllTasks();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
        }

        [Fact]
        public void GetTaskById_ShouldReturnTask()
        {
            // Arrange
            int taskId = 72; // Making sure this ID exists in  test database

            // Act
            var result = _taskRepo.GetTaskById(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Tasks>(result);
        }

        [Fact]
        public void AddTask_ShouldReturnRowsAffected()
        {
            // Arrange
            var task = TaskHelper.CreateTask();

            // Act
            var result = _taskRepo.AddTask(task);

            // Assert
            Assert.IsType<Tasks>(result);
        }

        [Fact]
        public void UpdateTask_ShouldTask()
        {
            // Arrange
            var task = TaskHelper.UpdateTask();

            // Act
            var result = _taskRepo.UpdateTask(task);

            // Assert
            Assert.IsType<Tasks>(result);
        }

        [Fact]
        public void DeleteTask_ShouldReturnRowsAffected()
        {
            // Arrange
            int taskId = 75; // Make sure this ID exists in test database

            // Act
            int result = _taskRepo.DeleteTask(taskId);

            // Assert
            Assert.True(result > 0);
        }
    }
}

