using Xunit;
using MyTamagotchi.Models;

namespace MyTamagotchi.Tests
{
    public class FoodItemTests
    {
        [Fact]
        public void Constructor_SetsNameAndImagePath()
        {
            var food = new FoodItem("Fisch", "fisch.png");

            Assert.Equal("Fisch", food.Name);
            Assert.Equal("fisch.png", food.ImagePath);
        }

        [Fact]
        public void ToString_ReturnsName()
        {
            var food = new FoodItem("Krabbe", "krabbe.png");

            Assert.Equal("Krabbe", food.ToString());
        }
    }
}
