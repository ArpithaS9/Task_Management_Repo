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
    public class UserControllerTest
    {
        private readonly Mock<IUser> _mockUserService;
        private readonly UsersController _controller;

        public UserControllerTest()
        {
            _mockUserService = new Mock<IUser>();
            _controller = new UsersController(_mockUserService.Object);
        }

        private DataTable CreateSampleUserDataTable(int id)
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

            dataTable.Rows.Add(id, "Test User", "User Description", "Pending", "High", 1, DateTime.Now, DateTime.Now);

            return dataTable;
        }

        #region GetAllUser
        [Fact]
        public void GetAllUser_ReturnsOkWithTask()
        {
            // Arrange
            int validId = 1;
            var dataTable = CreateSampleUserDataTable(validId);

            _mockUserService.Setup(repo => repo.GetAllUsers()).Returns(dataTable);

            // Act
            var result = _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDataTable = Assert.IsType<DataTable>(okResult.Value);
            Assert.Equal(dataTable.Rows.Count, returnedDataTable.Rows.Count);
            Assert.Equal(dataTable.Columns.Count, returnedDataTable.Columns.Count);
            _mockUserService.Verify(repo => repo.GetAllUsers(), Times.Once());

        }
        #endregion

        #region GetTaskById.Testcases

        [Fact]
        public void GetUser_ReturnsBadRequest_WhenIdIsInvalid()
        {
            // Arrange
            int invalidId = 0;

            // Act
            var result = _controller.GetUser(invalidId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid Id", badRequestResult.Value);
        }

        [Fact]
        public void GetUser_ReturnsNotFound_WhenUserIsNotFound()
        {
            // Arrange
            int validId = 1;
            User user = null;
            _mockUserService.Setup(service => service.GetUserById(validId)).Returns(user);
            // Act
            var result = _controller.GetUser(validId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetUser_ReturnsOkWithTask_WhenUserIsFound()
        {
            // Arrange
            int validId = 4;
            var validUser = UserHelper.UpdateUser();
            _mockUserService.Setup(repo => repo.GetUserById(validId)).Returns(validUser);

            // Act
            var result = _controller.GetUser(validId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedData = Assert.IsType<User>(okResult.Value);
            Assert.Equal(validId, returnedData.Id);
        }
        #endregion

        #region AddTask.TestCases
        [Fact]
        public void AddUser_ReturnsBadRequest_WhenUserIsNull()
        {
            // Act
            var result = _controller.CreateUser(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid Data", badRequestResult.Value);
        }

        [Fact]
        public void CreateUser_ReturnsOkWithCreatedTask_WhenUserIsCreatedSuccessfully()
        {
            // Arrange
            var validUser = UserHelper.CreateUser("HelloWorld@gmail.com");
            var createdUser = validUser;
            createdUser.Id = 1;

            _mockUserService.Setup(repo => repo.AddUser(validUser)).Returns(createdUser);

            // Act
            var result = _controller.CreateUser(validUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultTask = Assert.IsType<User>(okResult.Value);
            Assert.Equal(validUser, resultTask);
            _mockUserService.Verify(repo => repo.AddUser(validUser), Times.Once());
        }

        #endregion


        #region updateuser.TestCases
        [Fact]
        public void UpdateUser_ReturnsBadRequest_WhenIdDoesNotMatchUserId()
        {
            // Arrange
            int id = 1;
            var user = new User { Id = 2 }; // Ids do not match

            // Act
            var result = _controller.UpdateUser(id, user);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid ID", badRequestResult.Value);
        }

        [Fact]
        public void UpdateTask_ReturnsBadRequest_WhenIdIsZero()
        {
            // Arrange
            int id = 0;
            var user = new User { Id = 2 }; // Ids do not match

            // Act
            var result = _controller.UpdateUser(id, user);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid ID", badRequestResult.Value);
        }

        [Fact]
        public void UpdateUser_ReturnsBadRequest_WhenUserIsNull()
        {
            // Arrange
            int id = 1;
            //  User user = null ; // User is null

            // Act
            var result = _controller.UpdateUser(id, null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Provide User details to be updated", badRequestResult.Value);
        }

        [Fact]
        public void UpdateUser_ReturnsOkResult_WhenUserIsUpdatedSuccessfully()
        {
            // Arrange
            int id = 4;
            var validUser = UserHelper.UpdateUser();

            _mockUserService.Setup(repo => repo.UpdateUser(validUser)).Returns(validUser);

            // Act
            var result = _controller.UpdateUser(id, validUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(validUser, resultUser);

            _mockUserService.Verify(repo => repo.UpdateUser(validUser), Times.Once);
        }

        #endregion


        #region Delete.TestCases
        [Fact]
        public void DeleteUser_ReturnsBadRequest_WhenIdIsInvalid()
        {
            // Arrange
            int invalidId = 0;

            // Act
            var result = _controller.DeleteUser(invalidId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid Id", badRequestResult.Value);
        }
        [Fact]
        public void DeleteUser_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            int validId = 1;

            _mockUserService.Setup(repo => repo.DeleteUser(validId)).Verifiable();

            // Act
            var result = _controller.DeleteUser(validId);

            // Assert
            Assert.IsType<NoContentResult>(result);

            // Verify that DeleteTask was called exactly once
            _mockUserService.Verify(repo => repo.DeleteUser(validId), Times.Once);
        }

        #endregion
    }
}