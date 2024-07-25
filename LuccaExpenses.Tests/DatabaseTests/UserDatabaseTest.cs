namespace LuccaExpenses.Tests.DbInitializationTests
{
    [TestClass]
    public class UserDatabaseTest : BaseTests
    {

        [TestMethod]
        public void Test_User_Data_Initialized()
        {
            // Arrange
            var dbContext = Container.GetInstance<LuccaExpensesDbContext>();

            // Act
            var users = dbContext.User.ToList();

            // Assert
            Assert.AreEqual(2, users.Count);
            Assert.AreEqual("Anthony", users[0].FirstName);
            Assert.AreEqual("Natasha", users[1].FirstName);
        }
    }
}
