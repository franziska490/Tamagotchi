using Xunit;
using MyTamagotchi.Models;

namespace MyTamagotchi.Tests
{
    public class UserTests
    {
        [Fact]
        public void Constructor_SetsAllProperties()
        {
            // Arrange + Act
            var user = new User(1, "testuser", "secret123", "admin");

            // Assert
            Assert.Equal(1, user.Id);
            Assert.Equal("testuser", user.Username);
            Assert.Equal("secret123", user.Password);
            Assert.Equal("admin", user.Role);
        }

        [Fact]
        public void IsAdmin_ReturnsTrue_WhenRoleIsAdmin()
        {
            var user = new User(2, "adminUser", "pw", "admin");

            Assert.True(user.IsAdmin());
        }

        [Fact]
        public void IsAdmin_ReturnsFalse_WhenRoleIsNotAdmin()
        {
            var user = new User(3, "regularUser", "pw", "user");

            Assert.False(user.IsAdmin());
        }

        [Fact]
        public void ToString_ReturnsUsernameAndRole()
        {
            var user = new User(4, "laura", "pw", "admin");

            Assert.Equal("laura (admin)", user.ToString());
        }
    }
}
