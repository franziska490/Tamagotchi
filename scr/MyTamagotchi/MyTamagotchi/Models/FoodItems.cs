using System;
using System.Windows.Media.Imaging;

namespace MyTamagotchi.Models
{
    public class FoodItem
    {
        public string Name { get; set; }
        public int HungerRestore { get; set; }
        public int MoodBoost { get; set; }
        public string ImagePath { get; set; }

        public FoodItem(string name, int hungerRestore, int moodBoost, string imagePath)
        {
            Name = name;
            HungerRestore = hungerRestore;
            MoodBoost = moodBoost;
            ImagePath = imagePath;
        }

        public BitmapImage LoadImage()
        {
            return new BitmapImage(new Uri($"pack://application:,,,/Assets/Nom/{ImagePath}", UriKind.Absolute));
        }


        public void ApplyTo(Pet pet)
        {
            pet.Hunger = Math.Min(100, pet.Hunger + HungerRestore);
            pet.Mood = Math.Min(100, pet.Mood + MoodBoost);
        }

        public override string ToString() => Name;
    }
}
