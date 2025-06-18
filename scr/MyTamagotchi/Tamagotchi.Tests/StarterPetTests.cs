using Xunit;
using MyTamagotchi.Models;

namespace MyTamagotchi.Tests
{
    public class StarterPetTests
    {
        [Fact]
        public void Constructor_SetsTypeAndName_ChubbySeal()
        {
            var pet = new StarterPet(StarterType.ChubbySeal);

            Assert.Equal(StarterType.ChubbySeal, pet.Type);
            Assert.Equal("Chubby Seal", pet.Name);
        }

        [Fact]
        public void Constructor_SetsTypeAndName_Pinguin()
        {
            var pet = new StarterPet(StarterType.Pinguin);

            Assert.Equal(StarterType.Pinguin, pet.Type);
            Assert.Equal("Pinguin", pet.Name);
        }

        [Fact]
        public void SetActionImage_ChangesImagePath()
        {
            var pet = new StarterPet(StarterType.ChubbySeal);
            pet.SetActionImage("nom");

            Assert.Equal("/Assets/seal_nom.png", pet.ImagePath);
        }

        [Fact]
        public void UpdateImage_SetsDeadImage_IfStatZero()
        {
            var pet = new StarterPet(StarterType.Pinguin);
            pet.Hunger = 0;

            pet.UpdateImage();

            Assert.Equal("/Assets/pinguin_dead.png", pet.ImagePath);
        }
    }
}
