using System;
using System.Windows.Media.Imaging;

namespace MyTamagotchi.Models
{
    public enum StarterType
    {
        ChubbySeal,
        Pinguin
    }

    public class StarterPet : Pet
    {
        public StarterType Type { get; }
        public BitmapImage PetImage { get; private set; }

        public StarterPet(StarterType type) : base(GetNameFromType(type))
        {
            Type = type;
            UpdateImage();
        }

        private string GetPrefix()
        {
            return Type switch
            {
                StarterType.ChubbySeal => "seal",
                StarterType.Pinguin => "pinguin",
                _ => "seal"
            };
        }

        private BitmapImage LoadImage(string name)
        {
            return new BitmapImage(new Uri($"pack://application:,,,/Assets/{GetPrefix()}_{name}.png"));
        }

        public void UpdateImage()
        {
            if (Mood == 0 || Energy == 0 || Hunger == 0)
            {
                ImagePath = $"/Assets/{GetPrefix()}_dead.png";
                PetImage = LoadImage("dead");
                return;
            }

            if (Hunger < 50)
            {
                ImagePath = $"/Assets/{GetPrefix()}_hungry.png";
                PetImage = LoadImage("hungry");
                return;
            }

            if (Energy < 50)
            {
                ImagePath = $"/Assets/{GetPrefix()}_sleepy.png";
                PetImage = LoadImage("sleepy");
                return;
            }

            if (Mood < 70)
            {
                ImagePath = $"/Assets/{GetPrefix()}_sad.png";
                PetImage = LoadImage("sad");
                return;
            }

            ImagePath = $"/Assets/{GetPrefix()}_happy.png";
            PetImage = LoadImage("happy");
        }

        public void SetActionImage(string action)
        {
            ImagePath = $"/Assets/{GetPrefix()}_{action}.png";
            PetImage = LoadImage(action);
        }


        //public void SetActionImage(string action)
        //{
        //    PetImage = LoadImage(action);
        //}

        private static string GetNameFromType(StarterType type)
        {
            return type switch
            {
                StarterType.ChubbySeal => "Chubby Seal",
                StarterType.Pinguin => "Pinguin",
                _ => "Unbekannt"
            };
        }
    }
}
