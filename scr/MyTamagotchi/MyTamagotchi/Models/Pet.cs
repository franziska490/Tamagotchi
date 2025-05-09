using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTamagotchi.Models
{
    public class Pet : IFeedable, IPlayable, ISleepable
    {
        public string Name { get; set; }
        public int Hunger { get; set; }
        public int Energy { get; set; }
        public int Mood { get; set; }

        public Pet(string name)
        {
            Name = name;
            Hunger = 100;
            Energy = 100;
            Mood = 100;
        }

        public void Feed()
        {
            Hunger = Math.Min(Hunger + 10, 100);
        }

        public void Play()
        {
            Mood = Math.Min(Mood + 10, 100);
            Energy = Math.Max(Energy - 5, 0);
        }

        public void Sleep()
        {
            Energy = Math.Min(Energy + 15, 100);
            Hunger = Math.Max(Hunger - 5, 0);
        }
    }
}
