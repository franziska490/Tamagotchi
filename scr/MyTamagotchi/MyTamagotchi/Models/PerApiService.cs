using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyTamagotchi.Models
{
    public static class PetApiService
    {
        private static readonly HttpClient client = new HttpClient();

        // Basis-URL deiner API (später anpassen)
        private static readonly string baseUrl = "https://example.com/api/pets"; // FAKE-URL

        public static async Task<List<Pet>> GetPetsAsync()
        {
            try
            {
                // WICHTIG: Hier simulieren wir erstmal
                await Task.Delay(1000); // Simulation: Warten auf Antwort
                return new List<Pet> { new Pet("API-Fluffy") };
            }
            catch (Exception)
            {
                return new List<Pet>();
            }
        }

        public static async Task SavePetAsync(Pet pet)
        {
            try
            {
                await Task.Delay(1000); // Simulation
                // In echt:
                // await client.PostAsJsonAsync(baseUrl, pet);
            }
            catch (Exception)
            {
                // Fehler ignorieren
            }
        }
    }
}
