//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;
//using MyTamagotchi.Models;

//namespace MyTamagotchi.Tests
//{
//    public class PetTests
//    {
//        [Fact]
//        public void Feed_IncreasesHunger()
//        {
//            // Arrange
//            var pet = new Pet("TestPet");
//            pet.Hunger = 50;

//            // Act
//            pet.Feed();

//            // Assert
//            Assert.Equal(60, pet.Hunger);
//        }

//        [Fact]
//        public void Play_IncreasesMood_DecreasesEnergy()
//        {
//            // Arrange
//            var pet = new Pet("TestPet");
//            pet.Mood = 50;
//            pet.Energy = 50;

//            // Act
//            pet.Play();

//            // Assert
//            Assert.Equal(60, pet.Mood);
//            Assert.Equal(45, pet.Energy);
//        }

//        [Fact]
//        public void Sleep_IncreasesEnergy_DecreasesHunger()
//        {
//            // Arrange
//            var pet = new Pet("TestPet");
//            pet.Energy = 50;
//            pet.Hunger = 50;

//            // Act
//            pet.Sleep();

//            // Assert
//            Assert.Equal(65, pet.Energy);
//            Assert.Equal(45, pet.Hunger);
//        }
//    }
//}
