using AutoMapper;
using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Enums;
using LuccaExpenses.Api.MappingProfiles;
using LuccaExpenses.Api.Models;

namespace LuccaExpenses.Tests.MappingProfileTests
{
    [TestClass]
    public class UserMappingProfileTests : BaseTests
    {
        private readonly IMapper _mapper;

        public UserMappingProfileTests()
        {
            _mapper = Container.GetInstance<IMapper>();
        }

        [TestMethod]
        public void Should_Map_User_To_UserDto()
        {
            // Arrange
            User user = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Currency = "USD"
            };

            // Act
            UserDto userDto = _mapper.Map<UserDto>(user);

            // Assert
            Assert.AreEqual(user.Id, userDto.Id);
            Assert.AreEqual(user.FirstName, userDto.FirstName);
            Assert.AreEqual(user.LastName, userDto.LastName);
            Assert.AreEqual(Currency.USD, userDto.Currency);
        }

        [TestMethod]
        public void Should_Map_UserDto_To_User()
        {
            // Arrange
            UserDto userDto = new UserDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Currency = Currency.USD
            };

            // Act
            User user = _mapper.Map<User>(userDto);

            // Assert
            Assert.AreEqual(userDto.Id, user.Id);
            Assert.AreEqual(userDto.FirstName, user.FirstName);
            Assert.AreEqual(userDto.LastName, user.LastName);
            Assert.AreEqual(userDto.Currency.ToString(), user.Currency);
        }
    }
}
