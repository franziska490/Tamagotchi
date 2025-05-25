using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTamagotchi.Models
{
    public class TestApi
    {
        public async Task DisplayPets()
        {
            PetApiService api = new PetApiService();
            List<Pet> pets = await api.GetTestPets();
            foreach (Pet pet in pets)
            {
                Debug.WriteLine($"+++ PET INFOS: ");
                Debug.WriteLine($"ID: {pet.Id}, OwnerID: {pet.ownerid}, Name: {pet.Name}, Hunger: {pet.Hunger}, Energy: {pet.Energy}, Mood: {pet.Mood}");
            }
            

        }
        public static async Task Main(string[] args)
        {
            Debug.WriteLine("+++ MY INFO: Starte TestMain...");
            TestApi testMain = new TestApi();
            await testMain.DisplayPets();
        }
    }
}
