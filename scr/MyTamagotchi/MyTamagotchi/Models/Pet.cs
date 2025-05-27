using System;
using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace MyTamagotchi.Models
{
    public class Pet
    {
        [JsonPropertyName("petid")]
        public int Id { get; set; }
        public int ownerid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("hunger")]
        public int Hunger { get; set; }
        [JsonPropertyName("energy")]
        public int Energy { get; set; }
        [JsonPropertyName("mood")]
        public int Mood { get; set; }

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
            CheckGameOver();
        }

        public void Play()
        {
            Mood = Math.Min(Mood + 30, 100);
            Energy = Math.Max(Energy - 10, 0);
            CheckGameOver();
        }

        public void Sleep()
        {
            Energy = Math.Min(Energy + 50, 100);
            Hunger = Math.Max(Hunger - 5, 0);
            CheckGameOver();
        }

        public event Action? OnGameOver;

        public void CheckGameOver()
        {
            if (Hunger == 0 || Mood == 0 || Energy == 0)
            {
                OnGameOver?.Invoke();
            }
        }

    }
}
