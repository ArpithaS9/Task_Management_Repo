using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data;
using Task_Mangement.Controllers;
using Task_Mangement.Models;
using Task_Mangement.Repository;
using Task_Mangement.Task_Management_Test.TestHelper;
using Xunit;

namespace Task_Management.Tests.Contollers
{
    public class TaskControllerTest
    {
        private readonly Mock<ITask> _mockTaskService;
        private readonly UserController _controller;

        public TaskControllerTest()
        {
            _mockTaskService = new Mock<ITask>();
            _controller = new UserController(_mockTaskService.Object);
        }

        private DataTable CreateSampleTaskDataTable(int id)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));
            dataTable.Columns.Add("Priority", typeof(string));
            dataTable.Columns.Add("AssignedTo", typeof(int));
            dataTable.Columns.Add("CreatedAt", typeof(DateTime));
            dataTable.Columns.Add("UpdatedAt", typeof(DateTime));
            
            dataTable.Rows.Add(id, "Test Task", "Task Description", "Pending", "High", 1, DateTime.Now, DateTime.Now);

            return dataTable;
        }

        #region GetAllTasks
        [Fact]
        public void GetAllTasks_ReturnsOkWithTask()
        {
            // Arrange
            int validId = 1;
            var dataTable = CreateSampleTaskDataTable(validId);

            _mockTaskService.Setup(repo => repo.GetAllTasks()).Returns(dataTable);

            // Act
            var result = _controller.GetTasks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDataTable = Assert.IsType<DataTable>(okResult.Value);
            Assert.Equal(dataTable.Rows.Count, returnedDataTable.Rows.Count);
            Assert.Equal(dataTable.Columns.Count, returnedDataTable.Columns.Count);
            _mockTaskService.Verify(repo => repo.GetAllTasks(), Times.Once());

        }
        #endregion

        #region GetTaskById.Testcases

        [Fact]
        public void GetTask_ReturnsBadRequest_WhenIdIsInvalid()
        {
            // Arrange
            int invalidId = 0;

            // Act
            var result = _controller.GetTask(invalidId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid Id", badRequestResult.Value);
        }

        [Fact]
        public void GetTask_ReturnsNotFound_WhenTaskIsNotFound()
        {
            // Arrange
            int validId = 1;
            Tasks task = null;
            _mockTaskService.Setup(service => service.GetTaskById(validId)).Returns(task);
            // Act
            var result = _controller.GetTask(validId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetTask_ReturnsOkWithTask_WhenTaskIsFound()
        {
            // Arrange
            int validId = 72;
            var validTask = TaskHelper.UpdateTask();
            _mockTaskService.Setup(repo => repo.GetTaskById(validId)).Returns(validTask);

            // Act
            var result = _controller.GetTask(validId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedData= Assert.IsType<Tasks>(okResult.Value);
            Assert.Equal(validId, returnedData.Id);
        }
        #endregion

        #region AddTask.TestCases
        [Fact]
        public void AddTask_ReturnsBadRequest_WhenTaskIsNull()
        {
            // Act
            var result = _controller.CreateTask(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid Data", badRequestResult.Value);
        }

        [Fact]
        public void CreateTask_ReturnsOkWithCreatedTask_WhenTaskIsCreatedSuccessfully()
        {
            // Arrange
            var validTask = TaskHelper.CreateTask();
            var createdTask = validTask;
            createdTask.Id = 1;

            _mockTaskService.Setup(repo => repo.AddTask(validTask)).Returns(createdTask);

            // Act
            var result = _controller.CreateTask(validTask);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultTask = Assert.IsType<Tasks>(okResult.Value);
            Assert.Equal(createdTask, resultTask);
            _mockTaskService.Verify(repo=>repo.AddTask(validTask), Times.Once());
        }

        #endregion


        #region updatetask.TestCases
        [Fact]
        public void UpdateTask_ReturnsBadRequest_WhenIdDoesNotMatchTaskId()
        {
            // Arrange
            int id = 1;
            var task = new Tasks { Id = 2 }; // Ids do not match

            // Act
            var result = _controller.UpdateTask(id, task);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid ID", badRequestResult.Value);
        }

        [Fact]
        public void UpdateTask_ReturnsBadRequest_WhenIdIsZero()
        {
            // Arrange
            int id = 0;
            var task = new Tasks { Id = 2 }; // Ids do not match

            // Act
            var result = _controller.UpdateTask(id, task);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid ID", badRequestResult.Value);
        }

        [Fact]
        public void UpdateTask_ReturnsBadRequest_WhenTaskIsNull()
        {
            // Arrange
            int id = 1;
          //  Tasks task = null ; // Task is null

            // Act
            var result = _controller.UpdateTask(id, null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Provide Task details to be updated", badRequestResult.Value);
        }

        [Fact]
        public void UpdateTask_ReturnsOkResult_WhenTaskIsUpdatedSuccessfully()
        {
            // Arrange
            int id = 72;
            var validTask = TaskHelper.UpdateTask();

            _mockTaskService.Setup(repo => repo.UpdateTask(validTask)).Returns(validTask);

            // Act
            var result = _controller.UpdateTask(id, validTask);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultTask = Assert.IsType<Tasks>(okResult.Value);
            Assert.Equal(validTask, resultTask);

            _mockTaskService.Verify(repo => repo.UpdateTask(validTask), Times.Once);
        }

        #endregion


        #region Delete.TestCases
        [Fact]
        public void DeleteTask_ReturnsBadRequest_WhenIdIsInvalid()
        {
            // Arrange
            int invalidId = 0;

            // Act
            var result = _controller.DeleteTask(invalidId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid ID", badRequestResult.Value);
        }
        [Fact]
        public void DeleteTask_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            int validId = 1;

            _mockTaskService.Setup(repo => repo.DeleteTask(validId)).Verifiable();

            // Act
            var result = _controller.DeleteTask(validId);

            // Assert
            Assert.IsType<NoContentResult>(result);

            // Verify that DeleteTask was called exactly once
            _mockTaskService.Verify(repo => repo.DeleteTask(validId), Times.Once);
        }

        #endregion
    }
}