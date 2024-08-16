using Microsoft.Extensions.Configuration;
using System.Data;
using Task_Mangement.Models;
using Task_Mangement.Repository;
using Task_Mangement.Task_Management_Test.TestHelper;
using Xunit;

namespace Task_Management_Test.Repository
{
    public class UserRepoTest
    {
        private UserRepo _userRepo;

        public UserRepoTest()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", "Server=CTAADPG02J88E\\SQLEXPRESS2019;Database=TasKManagement;Trusted_Connection=true;TrustServerCertificate=true"}
            };
            var configuration = new ConfigurationBuilder()
              .AddInMemoryCollection(inMemorySettings)
              .Build();

            _userRepo = new UserRepo(configuration);
        }

        [Fact]
        public void GetAllUser_ShouldReturnDataTable()
        {
            // Act
            DataTable result = _userRepo.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DataTable>(result);
        }

        [Fact]
        public void GetUserkById_ShouldReturnUser()
        {
            // Arrange
            int userId = 28; // Making sure this ID exists in  test database

            // Act
            var result = _userRepo.GetUserById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
        }

        [Fact]
        public void AddUser_ShouldReturnRowsAffected()
        {
            // Arrange
            var user = UserHelper.CreateUser("Hello1@gmail.com");
            // Act
            var result = _userRepo.AddUser(user);

            // Assert
            Assert.IsType<User>(result);
        }

        [Fact]
        public void UpdateTask_ShouldTask()
        {
            // Arrange
            var user = UserHelper.UpdateUser();


            // Act
            var result = _userRepo.UpdateUser(user);

            // Assert
            Assert.IsType<User>(result);
        }

        [Fact]
        public void DeleteTask_ShouldReturnRowsAffected()
        {
            // Arrange
            int userId = 21; // Make sure this ID exists in test database

            // Act
            int result = _userRepo.DeleteUser(userId);

            // Assert
            Assert.True(result > 0);
        }
    }
}

