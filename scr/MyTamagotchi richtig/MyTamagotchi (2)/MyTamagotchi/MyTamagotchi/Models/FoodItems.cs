using System;
using System.Windows.Media.Imaging;

namespace MyTamagotchi.Models
{
    public class FoodItem
    {
        public string Name { get; set; }

        //Wenn Unterschiedliche Werte für Items benötigt werden, Aktivieren es fehlt dann aber noch die Logik im Pet.cs.
        //public int HungerRestore { get; set; } = 0;
        //public int MoodBoost { get; set; } = 0;
        public string ImagePath { get; set; }

        public FoodItem(string name, string imagePath)
        {
            Name = name;
            ImagePath = imagePath;
        }

        public BitmapImage LoadImage()
        {
            return new BitmapImage(new Uri($"pack://application:,,,/Assets/Nom/{ImagePath}", UriKind.Absolute));
        }

        public override string ToString() => Name;
    }
}
