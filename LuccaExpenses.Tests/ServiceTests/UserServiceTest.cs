using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Enums;
using LuccaExpenses.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LuccaExpenses.Tests.ServiceTests
{
    [TestClass]
    public class UserServiceTest : BaseTests
    {
        private IUserService _userService;
        private LuccaExpensesDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            // Utiliser le conteneur pour obtenir les instances des services nécessaires
            _userService = Container.GetInstance<IUserService>();
            _context = Container.GetInstance<LuccaExpensesDbContext>();
        }

        [TestMethod]
        public async Task GetUserAsync_ShouldReturnUserDto_WhenUserExists()
        {
            // Arrange
            var userId = 1;

            // Act
            UserDto result = await _userService.GetUserAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.Id);
            Assert.AreEqual("Anthony", result.FirstName);
            Assert.AreEqual("Stark", result.LastName);
            Assert.AreEqual(Currency.USD, result.Currency);
        }

        [TestMethod]
        public async Task GetUserAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 999;

            // Act
            UserDto result = await _userService.GetUserAsync(userId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task ValidateUser_ShouldReturnUserDtoAndNoError_WhenUserExists()
        {
            // Arrange
            int userId = 1;

            // Act
            (StringBuilder notfoundRequestMessage, UserDto? user) = await _userService.ValidateUser(userId);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(notfoundRequestMessage.ToString()));
            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.Id);
            Assert.AreEqual("Anthony", user.FirstName);
            Assert.AreEqual("Stark", user.LastName);
            Assert.AreEqual(Currency.USD, user.Currency);
        }

        [TestMethod]
        public async Task ValidateUser_ShouldReturnErrorMessage_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 999;

            // Act
            (StringBuilder notfoundRequestMessage, UserDto? user) = await _userService.ValidateUser(userId);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(notfoundRequestMessage.ToString()));
            Assert.IsNull(user);
            Assert.AreEqual($"User not found for userId : {userId}\r\n", notfoundRequestMessage.ToString());
        }
    }
}
