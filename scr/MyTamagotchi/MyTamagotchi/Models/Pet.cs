using System;


namespace MyTamagotchi.Models
{
    public class Pet
    {
        public string Name { get; set; }
        public int Hunger { get; set; } = 100;
        public int Mood { get; set; } = 100;
        public int Energy { get; set; } = 100;
        //public abstract string GetImagePath();


    
    
       

        // NEU: Sinkraten der Werte
        public int HungerDecreaseRate { get; set; } = 2;
        public int EnergyDecreaseRate { get; set; } = 2;
        public int MoodDecreaseRate { get; set; } = 1;

        public Pet(string name)
        {
            Name = name;
            Hunger = 100;
            Energy = 100;
            Mood = 100;
        }

        public void Feed()
        {
            Hunger = Math.Min(Hunger + 40, 100);
            Energy = Math.Max(Energy - 5, 0);
        }

        public void Play()
        {
            Mood = Math.Min(Mood + 30, 100);
            Energy = Math.Max(Energy - 10, 0);
        }

        public void Sleep()
        {
            Energy = Math.Min(Energy + 50, 100);
            Hunger = Math.Max(Hunger - 5, 0);
        }
    }
}
